using System;
using NUnit.Framework;
using Moq;
using Caros.Core.Context;
using System.Collections.Generic;
using System.Linq;

namespace Caros.Music.Tests
{
    [TestFixture]
    public class PlayerServiceTests
    {
        [TestCase]
        public void ShouldGetRecentTracksPlayed()
        {
            // input
            var tracksToPlay = CreateTestTracks(10);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = tracksToPlay.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            foreach (var trackToPlay in tracksToPlay)
            {
                player.Play(trackToPlay);
            }

            // compare
            var expected = tracksToPlay.Select(x => x.Model.HashName);
            var actual = player.GetRecentTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldReturnEmptyIfNoRecentTracksPlayed()
        {
            // input
            var tracksToPlay = CreateTestTracks(10);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = tracksToPlay.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            /// ..do nothing!

            // compare
            var expected = Enumerable.Empty<Track>();
            var actual = player.GetRecentTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldReturnOnlyPlayedItems()
        {
            // input
            var allTracks = CreateTestTracks(10);
            var tracksToPlay = allTracks.Take(3);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = allTracks.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            foreach (var trackToPlay in tracksToPlay)
            {
                player.Play(trackToPlay);
            }

            // compare
            var expected = tracksToPlay.Select(x => x.Model.HashName);
            var actual = player.GetRecentTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldIgnoreTracksInHistoryMissingFromLibrary()
        {
            // input
            var allTracks = CreateTestTracks(10);
            var localTracksToPlay = allTracks.Take(3);
            var tracksToPlay = localTracksToPlay.Concat(CreateTestTracks(2));

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = allTracks.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            foreach (var trackToPlay in tracksToPlay)
            {
                player.Play(trackToPlay);
            }

            // compare
            var expected = localTracksToPlay.Select(x => x.Model.HashName);
            var actual = player.GetRecentTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldGetUnplayedTracks()
        {
            // input
            var allTracks = CreateTestTracks(10);
            var tracksToPlay = allTracks.Take(3);
            var tracksNotPlayed = allTracks.Skip(3);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = allTracks.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            foreach (var trackToPlay in tracksToPlay)
            {
                player.Play(trackToPlay);
            }

            // compare
            var expected = tracksNotPlayed.Select(x => x.Model.HashName);
            var actual = player.GetUnheardTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldReturnEmptyIfNoUnheardTracks()
        {
            // input
            var allTracks = CreateTestTracks(10);
            var tracksToPlay = allTracks.Take(10);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = allTracks.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            foreach (var trackToPlay in tracksToPlay)
            {
                player.Play(trackToPlay);
            }

            // compare
            var expected = Enumerable.Empty<Track>();
            var actual = player.GetUnheardTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldReturnAllTracksIfAllUnheard()
        {
            // input
            var allTracks = CreateTestTracks(10);

            // mock
            var context = new ApplicationContext();

            var mockPlayerService = new Mock<PlayerService>(context) { CallBase = true };
            var player = mockPlayerService.Object;
            player.TracksCollection = allTracks.ToList();

            var mockMediaPlayer = new Mock<IMediaPlayer>();
            player.MediaPlayer = mockMediaPlayer.Object;

            var mockHistoryManager = new Mock<History>(context) { CallBase = true };
            mockHistoryManager.Setup(x => x.AddToDatabase(It.IsAny<HistoryModel>()));
            player.HistoryManager = mockHistoryManager.Object;

            // act
            // ... do nothing

            // compare
            var expected = allTracks.Select(x => x.Model.HashName);
            var actual = player.GetUnheardTracks().Select(x => x.Model.HashName);

            CollectionAssert.AreEqual(expected, actual);
        }

        static int hashCodeIndex;
        private IReadOnlyCollection<Track> CreateTestTracks(int amount)
        {
            var results = new List<Track>();

            for (int i = 0; i < amount; i++)
            {
                var trackModel = new TrackModel { HashName = (++hashCodeIndex).ToString() };
                var mockTrack = new Mock<Track>(trackModel);
                
                mockTrack
                    .Setup(x => x.GetUri(It.IsAny<IContext>()))
                    .Returns(It.IsAny<Uri>());

                results.Add(mockTrack.Object);
            }

            return results;
        }
    }
}
