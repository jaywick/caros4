using System;
using NUnit.Framework;
using Moq;
using Caros.Core.Context;
using System.Collections.Generic;
using System.Linq;
using Caros.Core.Extensions;

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
            var expected = tracksToPlay;
            var actual = player.GetRecentTracks();

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
            var actual = player.GetRecentTracks();

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
            var expected = tracksToPlay;
            var actual = player.GetRecentTracks();

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
            var expected = localTracksToPlay;
            var actual = player.GetRecentTracks();

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
            var expected = tracksNotPlayed;
            var actual = player.GetUnheardTracks();

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
            var actual = player.GetUnheardTracks();

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
            var expected = allTracks;
            var actual = player.GetUnheardTracks();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldOrderPlayedTracksByPlayCountOnGetFavourites()
        {
            // input
            var allTracks = CreateTestTracks(10).ToList();

            var tracksToPlay = new[]
            {
                allTracks[3],
                allTracks[7],
                allTracks[3],
                allTracks[3],
                allTracks[7],
                allTracks[0]
            };

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
            var expected = new[] { allTracks[3], allTracks[7], allTracks[0] };
            var actual = player.GetFavouriteTracks();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestCase]
        public void ShouldNeverIncludeUnplayedInFavourites()
        {
            // input
            var allTracks = CreateTestTracks(10).ToList();

            var tracksToPlay = new[]
            {
                allTracks[3],
                allTracks[7],
                allTracks[3],
                allTracks[3],
                allTracks[7],
                allTracks[0]
            };

            var tracksNotToPlay = allTracks.SymmetricDifference(tracksToPlay);

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
            var notexpected = tracksNotToPlay;
            var actual = player.GetFavouriteTracks();

            CollectionAssert.DoesNotContain(notexpected, actual);
        }

        [TestCase]
        public void ShouldReturnEmptyIfNoPlaysOnGetFavourites()
        {
            // input
            var allTracks = CreateTestTracks(10).ToList();

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
            var expected = Enumerable.Empty<Track>();
            var actual = player.GetFavouriteTracks();

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
