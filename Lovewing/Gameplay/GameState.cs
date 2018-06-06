using System;
using System.Collections.Generic;
using System.Text;
using Lovewing.Beatmaps;

namespace Lovewing.Gameplay
{
    class GameState
    {
        public class NoteState
        {
            public uint Index; // Unique index of the note
            public uint LaneIndex; // The lane this note is in
            public double SpawnTime; // The time the note should spawn
            public double HitTime; // The time the note should hit the target circle
        }

        private List<NoteState> pendingNotes = new List<NoteState>();

        public bool Finished
        {
            get => pendingNotes.Count == 0;
        }

        public GameState(Beatmap beatmap, double noteSpeed)
        {
            uint index = 0;
            foreach(Note note in beatmap.Notes)
            {
                if(note.Position > 8)
                {
                    throw new Exception("Invalid note position for note at index " + index);
                }

                pendingNotes.Add(new NoteState
                {
                    LaneIndex = note.Position,
                    Index = index,
                    HitTime = note.Time * 1000.0,
                    SpawnTime = (note.Time - noteSpeed) * 1000.0
                });

                index++;
            }

            // Sort so the latest note is actually last in the list
            pendingNotes.Sort((note1, note2) => note2.SpawnTime.CompareTo(note1.SpawnTime));
        }

        public void Update(double time, Action<NoteState> spawnNote)
        {
            if(pendingNotes.Count == 0)
            {
                return; // No notes to spawn
            }

            NoteState nextNote = pendingNotes[pendingNotes.Count - 1];
            while (nextNote.SpawnTime <= time)
            {
                if(nextNote.HitTime >= time)
                {
                    spawnNote(nextNote);
                }
                pendingNotes.RemoveAt(pendingNotes.Count - 1);

                if(pendingNotes.Count == 0)
                {
                    return; // Hit the end of the list
                }
                nextNote = pendingNotes[pendingNotes.Count - 1];
            }
        }
    }

}
