using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Lovewing.Beatmaps.Loaders
{
    class MidiEvent
    {
        public enum Type
        {
            Note,
            Meta
        }

        public Type EventType { get; set; } = Type.Note;
        public bool Note { get; set; } = false;
        public int Position { get; set; } = 0;
        public int Velocity { get; set; } = 0;
        public int Channel { get; set; } = 0;

        public int Tick { get; set; } = 0;
        public int Order { get; set; } = 0;

        public int Meta { get; set; } = 0;
        public byte[] Data { get; set; }
    }

    class MidiParseContext
    {
        /// <summary>
        /// Events dictionary built up by calls to InsertMidiEvent
        /// </summary>
        private Dictionary<int, List<MidiEvent>> Events = new Dictionary<int, List<MidiEvent>>();

        /// <summary>
        /// Flattened events dictionary.
        /// </summary>
        private List<MidiEvent> FlattenedEvents = new List<MidiEvent>();

        /// <summary>
        /// Queue of on and off notes in specific channels.
        /// </summary>
        private Dictionary<int, MidiEvent> LongnoteQueue = new Dictionary<int, MidiEvent>();

        /// <summary>
        /// The base note index, used to determine the position of notes.
        /// </summary>
        private int BaseIndex;

        /// <summary>
        /// The current tempo of the midi file. This can be changed via tempo events.
        /// The default temp is 120 BPM
        /// </summary>
        private int Tempo { get; set; } = 120;

        /// <summary>
        /// The MIDIs delta time definition.
        /// </summary>
        public int PPQN
        {
            get
            {
                return _PPQN;
            }

            set
            {
                if (value >= 32768)
                {
                    throw new Exception("PPQN is out of range");
                }
                _PPQN = value;
            }
        }
        private int _PPQN = 0;

        /// <summary>
        /// Add a midi event to the parse context.
        /// </summary>
        /// <param name="tick">The tick time of the event.</param>
        /// <param name="data">The event data.</param>
        public void InsertMidiEvent(int tick, MidiEvent data)
        {
            List<MidiEvent> list;

            try
            {
                list = Events[tick];
            }
            catch (Exception)
            {
                list = new List<MidiEvent>();
                Events[tick] = list;
            }

            list.Add(data);
        }

        /// <summary>
        /// Convert all provided MidiEvents to a beatmap.
        /// </summary>
        /// <returns>The generated beatmap.</returns>
        public Beatmap ToBeatmap()
        {
            Beatmap beatmap = new Beatmap();

            FlattenEvents();

            var topIndex = 0;
            BaseIndex = 127;

            foreach (var item in FlattenedEvents)
            {
                if (item.EventType == MidiEvent.Type.Note)
                {
                    topIndex = Math.Max(topIndex, item.Position);
                    BaseIndex = Math.Min(BaseIndex, item.Position);
                }
            }

            var midIndex = topIndex - BaseIndex + 1;

            if (midIndex <= 9 && midIndex % 2 == 1)
            {
                throw new Exception("Failed to analyze note position. Make sure you only use 9 note keys or odd amount of note keys");
            }

            // If it's not 9 and it's odd, automatically adjust
            if (midIndex != 9 && midIndex % 2 == 1)
            {
                var midPos = (topIndex + BaseIndex) / 2;
                topIndex = midPos + 4;
                BaseIndex = midPos - 4;
            }

            ParseEvents(beatmap);

            return beatmap;
        }

        /// <summary>
        /// Populates the FlattenedEvents list by flattening the Events dictionary.
        /// </summary>
        void FlattenEvents()
        {
            // Flatten the dictionary of events
            foreach (var pair in Events)
            {
                for (var i = 0; i < pair.Value.Count; i++)
                {
                    MidiEvent midiEvent = pair.Value[i];

                    midiEvent.Tick = pair.Key;
                    midiEvent.Order = i;

                    FlattenedEvents.Add(midiEvent);
                }
            }

            // Now sort it by tick first, then order.
            FlattenedEvents.OrderByDescending(midiEvent => midiEvent.Tick)
                .ThenByDescending(midiEvent => midiEvent.Order);
        }

        /// <summary>
        /// Parses a meta midi event.
        /// </summary>
        /// <param name="midiEvent">The meta midi event.</param>
        void ParseMetaEvent(Beatmap beatmap, MidiEvent midiEvent)
        {
            // Currently only parsing tempo changes.
            switch(midiEvent.Meta)
            {
                case 81:
                    // Tempo event
                    if (midiEvent.Data.Length > 4)
                    {
                        throw new Exception("Tempo change event oversized");
                    }

                    Tempo = 0;

                    foreach (byte b in midiEvent.Data)
                    {
                        // Form an integer from each byte.
                        Tempo = (Tempo << 8) | b;
                    }

                    Tempo = 600000000 / Tempo / 10;
                    break;

                default:
                    break; // Ignored
            }
        }

        /// <summary>
        /// Parses a note midi event.
        /// </summary>
        /// <param name="midiEvent">The note midi event.</param>
        void ParseNoteEvent(Beatmap beatmap, MidiEvent midiEvent)
        {
            Note note = new Note()
            {
                Position = (uint)(midiEvent.Position - BaseIndex) + 1,
                Attribute = (uint)(midiEvent.Channel / 4),
                Effect = (uint)(midiEvent.Channel & 3) + 1
            };

            bool isSwing = midiEvent.Velocity < 64;

            if (note.Attribute > 0)
            {
                if (note.Effect == 3)
                {
                    if (midiEvent.Note)
                    {
                        // Add to longnote queue
                        if (LongnoteQueue.ContainsKey((int)note.Position))
                        {
                            throw new Exception("Another note in pos " + note.Position + " is in queue.");
                        }

                        LongnoteQueue[(int)note.Position] = midiEvent;

                    }
                    else
                    {
                        // note
                        note.Time = midiEvent.Tick * 60 / PPQN / Tempo;
                        note.Level = (uint)(isSwing ? midiEvent.Velocity : 0);
                        note.Effect = note.Effect + (isSwing ? 10U : 0U);
                        note.EffectValue = 2;

                        beatmap.Notes.Add(note);
                    }
                }
                else if (midiEvent.Note)
                {
                    // Stop longnote queue.
                    if (!LongnoteQueue.ContainsKey((int)note.Position))
                    {
                        throw new Exception("Queue for pos " + note.Position + " is empty.");
                    }

                    MidiEvent queued = LongnoteQueue[(int)note.Position];
                    isSwing = queued.Velocity < 64;

                    LongnoteQueue.Remove((int)note.Position);

                    note.Time = queued.Tick * 60 / PPQN / Tempo;
                    note.Level = (uint)(isSwing ? queued.Velocity + 1 : 1);
                    note.Effect = isSwing ? 13U : 3U;
                    note.EffectValue = (midiEvent.Tick - queued.Tick) * 60 / PPQN / Tempo;

                    beatmap.Notes.Add(note);
                }
            }
        }

        /// <summary>
        /// Parses all FlattenedEvents in to beatmap notes.
        /// </summary>
        /// <param name="beatmap">The beatmap to parse notes into.</param>
        void ParseEvents(Beatmap beatmap)
        {
            foreach (MidiEvent midiEvent in FlattenedEvents)
            {
                switch (midiEvent.EventType)
                {
                    case MidiEvent.Type.Meta:
                        ParseMetaEvent(beatmap, midiEvent);
                        break;

                    case MidiEvent.Type.Note:
                        ParseNoteEvent(beatmap, midiEvent);
                        break;
                }
            }

            // Ensure notes are sorted in the output beatmap.
            beatmap.Notes.Sort((note1, note2) => note1.Time.CompareTo(note2.Time));
        }
    }

    public class MIDLoader : IBeatmapLoader
    {
        private const string Header = "MThd";
        private const string TrackHeader = "MTrk";

        public string GetFileExtension()
        {
            return ".mid";
        }

        public bool CanLoadFile(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == GetFileExtension();
        }

        /// <summary>
        /// Converts a byte array to a 32 bit word.
        /// </summary>
        /// <param name="data">The array to read 4 bytes from.</param>
        /// <param name="index">The starting index of the data in the array.</param>
        /// <returns></returns>
        private static int BytesToDWord(byte[] data, uint index)
        {
            return (data[index] << 24) | (data[index + 1] << 16) | (data[index + 2] << 8) | data[index + 3];
        }

        /// <summary>
        /// Parses a MIDI varint out of the stream.
        /// VarInts are up to 32 bits long.
        /// Each byte of the varint contains 7 number bits (low bits), with the most significant
        /// bit acting as a flag. When this bit is set to 0, it means the current byte is the last
        /// one in the VarInt.
        /// </summary>
        /// <see cref="https://www.csie.ntu.edu.tw/~r92092/ref/midi/#vlq"/>
        /// <param name="stream"></param>
        /// <returns>The parsed VarInt value.</returns>
        private static int ParseVarInt(Stream stream)
        {
            int value = 0;
            int b;

            do
            {
                // Shift existing value left 7 places,
                // then add in the low 7 bits from the next byte
                // until we hit a byte with a 1 in the most significant bit.
                b = stream.ReadByte();
                value = (value << 7) | b & 127;
            }
            while ((b & 128) > 0);

            return value;
        }

        /// <summary>
        /// Checks the current point in the provided stream for a MIDI file header.
        /// The midi header will be equal to Header.
        /// </summary>
        /// <exception cref="Exception">Thrown if the stream is not sitting on a valid MIDI header.</exception>
        /// <param name="stream"></param>
        private static void CheckHeader(Stream stream)
        { 
            byte[] data = new byte[4];

            stream.Read(data, 0, 4);

            // First four bytes must be equal to the header
            var actual = Encoding.UTF8.GetString(data, 0, 4);
            if (actual != Header)
            {
                throw new Exception("File it not midi.");
            }

            stream.Read(data, 0, 4);

            // Header size is not 6
            var size = BytesToDWord(data, 0);
            if (size != 6)
            {
                throw new Exception("Header size is not 6.");
            }

            // Skip over the next two bytes
            stream.Seek(2L, SeekOrigin.Current);
        }

        /// <summary>
        /// Checks the current point in the provided stream for a MIDI track header.
        /// The track header will be equal to TrackHeader.
        /// </summary>
        /// <exception cref="Exception">Thrown if the stream is not sitting on a valid track header.</exception>
        /// <param name="stream">The current parsing stream</param>
        private static void CheckTrackHeader(Stream stream)
        {
            byte[] data = new byte[4];
        
            stream.Read(data, 0, 4);

            if (Encoding.UTF8.GetString(data, 0, 4) != TrackHeader)
            {
                throw new Exception("Invalid track header");
            }
        }

        /// <summary>
        /// Parses a track out of the current stream, placing the resulting notes in to the provided MidiParseContext
        /// </summary>
        /// <param name="context">The MidiParseContext to load notes into.</param>
        /// <param name="stream">The stream to read data from.</param>
        private void ParseTrack(MidiParseContext context, Stream stream)
        {
            int timingTotal = 0;

            while (stream.Position < stream.Length)
            {
                // Parse out each note def from the track.
                // Notes start with a timing VarInt, followed by an event type.
                var timing = ParseVarInt(stream);
                var eventByte = stream.ReadByte();
                var eventType = eventByte >> 4; // Upper half of the event byte is the type

                timingTotal += timing;

                if (eventType == 8 || eventType == 9)
                {
                    // Note event. 8 for note released, 9 for note held.
                    // Note events are a track position and a velocity.
                    // The low 4 bits in the event byte determine the channel that the note is played on.
                    MidiEvent midiEvent = new MidiEvent
                    {
                        EventType = MidiEvent.Type.Note,
                        Note = eventType == 8, // Whether this is held down, or released.
                        Channel = eventByte % 16
                    };

                    midiEvent.Position = stream.ReadByte();
                    midiEvent.Velocity = stream.ReadByte();

                    context.InsertMidiEvent(timingTotal, midiEvent);
                }
                else if (eventByte == 255)
                {
                    // Meta info event.
                    // Meta events are a meta type, data size, then data block.
                    MidiEvent midiEvent = new MidiEvent
                    {
                        EventType = MidiEvent.Type.Meta,
                        Meta = stream.ReadByte(),
                    };

                    // Work out how much data we have to load, then pull it in
                    var dataSize = ParseVarInt(stream);
                    midiEvent.Data = new byte[dataSize];
                    stream.Read(midiEvent.Data, 0, dataSize);

                    context.InsertMidiEvent(timingTotal, midiEvent);
                }
                else if (eventByte == 240 || eventByte == 247)
                {
                    // Ignore sysex events
                    while (stream.ReadByte() != 247) { };
                }
                else
                {
                    // Skip over other event types.
                    stream.Seek(2, SeekOrigin.Current);
                }
            }
        }

        /// <summary>
        /// Parses a midi from the provided stream.
        /// Returns a MidiParseContext containing all of the data parsed in from the stream.
        /// </summary>
        /// <param name="stream">Stream to read midi data from.</param>
        /// <returns>MidiParseContext with the parsed files</returns>
        private MidiParseContext ParseMidi(Stream stream)
        {
            MidiParseContext context = new MidiParseContext();
            byte[] data = new byte[4];

            // Confirm this is an actual midi file first
            CheckHeader(stream);

            // First 2 bytes is the track count.
            stream.Read(data, 2, 2);
            var trackCount = BytesToDWord(data, 0);

            // Next two bytes is the delta time.
            stream.Read(data, 2, 2);
            context.PPQN = BytesToDWord(data, 0);

            // Read out each track.
            for(int i = 0; i < trackCount; i++)
            {
                // Confirm the track starts with the track header.
                CheckTrackHeader(stream);

                // Grab the size of track, then pull out the track data.
                // Parse each track as an individual stream.
                stream.Read(data, 0, 4);
                var trackLength = BytesToDWord(data, 0);

                byte[] trackData = new byte[trackLength];
                stream.Read(trackData, 0, trackLength);

                // Form a new stream for each track and parse it.
                using(MemoryStream trackStream = new MemoryStream(trackData))
                {
                    ParseTrack(context, trackStream);
                }
            }

            return context;
        }

        public async Task<Beatmap> Load(string path)
        {
            byte[] data = await AsyncFileUtils.ReadBinaryFile(path);
            MidiParseContext context;

            using (var stream = new MemoryStream(data))
            {
                // Parse the midi into a parse context.
                context = ParseMidi(stream);
            }

            // Our parse context contains all of the relevant note data from the midi
            // Export it to a beatmap.
            return context.ToBeatmap();
        }
    }
}
