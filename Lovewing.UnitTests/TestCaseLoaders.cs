using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lovewing.Game.Loaders;

namespace Lovewing.UnitTests
{
    [TestClass]
    public class TestCaseLoaders
    {
        /// <summary>
        /// Tests if the provided collection is sorted according to the provided sort key.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">A function that returns a sort key for each item in the collection.</param>
        /// <returns>True if the collection is sorted, false if not.</returns>
        private bool IsSorted<T>(ICollection<T> collection, Func<T, IComparable> key)
        {
            for(var i = 0; i < collection.Count - 1; i++)
            {
                var current = collection.ElementAt(i);
                var next = collection.ElementAt(i + 1);

                if(key(current).CompareTo(key(next)) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public async Task TextFileAsyncTest()
        {
            var text = await AsyncFileUtils.ReadTextFile("../../Resources/text.txt");
            Assert.AreEqual("this is a test text file", text);

            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() =>
            {
                return AsyncFileUtils.ReadTextFile("this_file_doesn't_exist.txt");
            });
        }

        [TestMethod]
        public async Task BinaryFileAsyncTest()
        {
            var bin = await AsyncFileUtils.ReadBinaryFile("../../Resources/binary.bin");
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Assert.IsTrue(bin.SequenceEqual(expected));
        }

        [TestMethod]
        public async Task LLPLoaderTest()
        {
            var loader = new LLPLoader();

            Assert.AreEqual(".llp", loader.GetFileExtension());
            Assert.IsTrue(loader.CanLoadFile("../../Resources/Beatmaps/llp/let_me_hear.llp"));
            Assert.IsTrue(loader.CanLoadFile("../../Resources/Beatmaps/llp/private_wars.llp"));
            Assert.IsTrue(loader.CanLoadFile("../../Resources/Beatmaps/llp/stand_up.llp"));

            var beatmap = await loader.Load("../../Resources/Beatmaps/llp/let_me_hear.llp");
            Assert.AreEqual("tL0IRtvENu7GFWdP.wav", beatmap.SourceFile);
            Assert.AreEqual(192.0, beatmap.BPM);
            Assert.AreEqual(596, beatmap.Notes.Count);
            Assert.IsTrue(beatmap.Notes.All(note => note.Time > 0.0));
            Assert.IsTrue(IsSorted(beatmap.Notes, note => note.Time));

            beatmap = await loader.Load("../../Resources/Beatmaps/llp/private_wars.llp");
            Assert.AreEqual("G5rJhs4a1h4B2v7N.wav", beatmap.SourceFile);
            Assert.AreEqual(160.0, beatmap.BPM);
            Assert.AreEqual(504, beatmap.Notes.Count);
            Assert.IsTrue(beatmap.Notes.All(note => note.Time > 0.0));
            Assert.IsTrue(IsSorted(beatmap.Notes, note => note.Time));

            beatmap = await loader.Load("../../Resources/Beatmaps/llp/stand_up.llp");
            Assert.AreEqual("2o8bOtehEbonP11b.wav", beatmap.SourceFile);
            Assert.AreEqual(190.0, beatmap.BPM);
            Assert.AreEqual(885, beatmap.Notes.Count);
            Assert.IsTrue(beatmap.Notes.All(note => note.Time > 0.0));
            Assert.IsTrue(IsSorted(beatmap.Notes, note => note.Time));
        }

        [TestMethod]
        public async Task SIFTLoaderTest()
        {
            var loader = new SIFTLoader();
            var files = Directory.EnumerateFiles("../../Resources/Beatmaps/rs");

            Assert.AreEqual(".rs", loader.GetFileExtension());
            Assert.IsTrue(files.All(loader.CanLoadFile));

            // TODO: Better tests that check if the loaded data is correct.
            foreach (var file in files)
            {
                var beatmap = await loader.Load(file);
                Assert.IsTrue(beatmap.Notes.Count > 0, "Beatmap has notes");
                Assert.IsTrue(IsSorted(beatmap.Notes, note => note.Time), "Beatmap notes are sorted according to time");
            }
        }

        [TestMethod]
        public async Task MIDILoaderTest()
        {
            var loader = new MIDLoader();

            Assert.AreEqual(".mid", loader.GetFileExtension());
            Assert.IsTrue(loader.CanLoadFile("../../Resources/sample.mid"));

            var beatmap = loader.Load("../../Resources/sample.mid");
        }
    }
}
