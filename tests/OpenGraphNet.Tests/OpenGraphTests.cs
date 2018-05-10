﻿namespace OpenGraphNet.Tests
{
    // ReSharper disable StringLiteralTypo
    using System.Linq;
    using System.Threading.Tasks;

    using OpenGraphNet;

    using Xunit;

    /// <summary>
    /// The open graph test fixture
    /// </summary>
    public class OpenGraphTests
    {
        /// <summary>
        /// The spaced link
        /// </summary>
        private const string SpacedLink = "http://www.imdb.com/title/tt0187664/";

        private const string SpotifyAlbumContent = @"<!DOCTYPE html>
<html lang=en class="""">
<head>
    <meta charset=""UTF-8"">
    <meta property=""og:title"" content=""Salutations"">
    <meta property=""og:description"" content="""">
    <meta property=""og:url"" content=""https://open.spotify.com/album/5YQGQfkjghbxW00eKy9YpJ"">
    <meta property=""og:image"" content="""">
    <meta property=""og:type"" content=""music.album"">
    <meta property=""music:musician"" content=""https://open.spotify.com/artist/2Z7gV3uEh1ckIaBzTUCE6R"">
    <meta property=""music:release_date"" content=""2017-03-17"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/1JJUbiYekbYkdDhK1kp3C9"">
    <meta property=""music:song:disc"" content=""1"">
    <meta property=""music:song:track"" content=""1"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/3eitV6XbyRW0FxKEUh60Pi"">
    <meta property=""music:song:disc"" content=""1"">
    <meta property=""music:song:track"" content=""2"">
    <title>Spotify Web Player</title>
    <link rel=""icon"" href=""https://open.scdn.co/static/images/favicon.png "">
    <link rel=""stylesheet"" href=""https://open.scdn.co/static/web-player.97c0abb0.css"" />
    <link rel=""manifest"" href=""/static/manifest.json"">
    <link rel=""preconnect"" href=""https://api.spotify.com"">
    <link rel=""preconnect"" href=""https://spclient.wg.spotify.com"">
    <link rel=""preconnect"" href=""https://apresolve.spotify.com"">
</head>
<body class=""env-prod "" data-locale=""en"" data-market=""US""></body></html>";

        private const string SpotifyPlaylistContent = @"<!DOCTYPE html>
<html lang=en class="""">
<head>
    <meta charset=""UTF-8"">
    <meta property=""og:title"" content=""Programming Jams, a playlist by Jefe on Spotify"">
    <meta property=""og:description"" content="""">
    <meta property=""og:url"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3"">
    <meta property=""og:image"" content="""">
    <meta property=""og:type"" content=""music.playlist"">
    <meta property=""music:creator"" content=""https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe"">
    <meta property=""music:song_count"" content=""1020"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/3RL1cNdki1AsOLCMinb60a"">
    <meta property=""music:song:track"" content=""1"">
    <meta property=""music:song"" content=""https://open.spotify.com/track/4yVfG04odefa7JanoF5r86"">
    <meta property=""music:song:track"" content=""2"">
    <meta property=""og:restrictions:country:allowed"" content=""AD"">
    <meta property=""og:restrictions:country:allowed"" content=""AR"">
    <title>Spotify Web Player</title>
    <link rel=""icon"" href=""https://open.scdn.co/static/images/favicon.png "">
    <link rel=""stylesheet"" href=""https://open.scdn.co/static/web-player.97c0abb0.css"" />
    <link rel=""manifest"" href=""/static/manifest.json"">
    <link rel=""preconnect"" href=""https://api.spotify.com"">
    <link rel=""preconnect"" href=""https://spclient.wg.spotify.com"">
    <link rel=""preconnect"" href=""https://apresolve.spotify.com"">
    </head>
<body class=""env-prod "" data-locale=""en"" data-market=""US""></body>
</html>";

        /// <summary>
        /// The valid sample content
        /// </summary>
        private string validSampleContent = @"<!DOCTYPE HTML>
<html>
<head prefix=""og: http://ogp.me/ns# product: http://ogp.me/ns/product#"">
    <meta property=""og:type"" content=""product"" />
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta name=""og:image"" content=""http://www.test.com/test.png""/>
    <meta propErty=""og:uRl"" content=""http://www.test.com"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
</head>
<body>
</body>
</html>";

        /// <summary>
        /// The invalid sample content
        /// </summary>
        private string invalidSampleContent = @"<!DOCTYPE HTML>
<html>
<head>
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta name=""og:image"" content=""http://www.test.com/test.png""/>
    <meta propErty=""og:uRl"" content=""http://www.test.com"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
    <meta property=""og:mistake"" value=""not included"">
</head>
<body>
</body>
</html>";

        /// <summary>
        /// The invalid missing required URLs
        /// </summary>
        private string invalidMissingRequiredUrls = @"<!DOCTYPE HTML>
<html>
<head>
    <meta property=""og:type"" content=""product"" />
    <meta property=""og:title"" cOntent=""Product Title"" />
    <meta property=""og:description"" content=""My Description""/>
    <meta property=""og:site_Name"" content=""Test Site"">
</head>
<body>
</body>
</html>";

        /// <summary>
        /// The invalid missing all meta
        /// </summary>
        private string invalidMissingAllMeta = @"<!DOCTYPE HTML>
<html>
<head>
    <title>some title</title>
</head>
<body>
</body>
</html>";

        /////// <summary>
        /////// Tests the parsing spotify playlist.
        /////// </summary>
        ////[Fact]
        ////public void TestParsingSpotifyPlaylist()
        ////{
        ////    var graph = OpenGraph.ParseHtml(SpotifyPlaylistContent);

        ////    Assert.Equal("Programming Jams, a playlist by Jefe on Spotify", graph.Title);
        ////    Assert.Equal(string.Empty, graph.Data["description"].First().Value);
        ////    Assert.Equal("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe/playlist/2lzTTRqhYS6AkHPIvdX9u3", graph.Url.ToString());
        ////    Assert.Null(graph.Image);
        ////    Assert.Equal("music.playlist", graph.Type);
        ////    Assert.Equal("https://open.spotify.com/user/er811nzvdw2cy2qgkrlei9sqe", graph.Data["music:creator"].First().Value);
        ////    Assert.Equal("1020", graph.Data["music:song_count"].First().Value);
        ////    Assert.Equal(2, graph.Data["music:song"].Count);
        ////    Assert.Equal("https://open.spotify.com/track/3RL1cNdki1AsOLCMinb60a", graph.Data["music:song"][0].Value);
        ////    Assert.Equal("1", graph.Data["music:song"][0].Properties["track"].First().Value);
        ////    Assert.Equal("https://open.spotify.com/track/4yVfG04odefa7JanoF5r86", graph.Data["music:song"][1].Value);
        ////    Assert.Equal("2", graph.Data["music:song"][1].Properties["track"].First().Value);
        ////    Assert.Equal("AD", graph.Data["restrictions:country:allowed"][0].Value);
        ////    Assert.Equal("AR", graph.Data["restrictions:country:allowed"][1].Value);
        ////}

        /// <summary>
        /// Tests calling <c>MakeOpenGraph</c> method
        /// </summary>
        [Fact]
        public void TestMakingOpenGraphMetaTags()
        {
            var title = "some title";
            var type = "website";
            var image = "http://www.go.com/someimg.jpg";
            var url = "http://www.go.com/";
            var description = "some description";
            var siteName = "my site";
            var graph = OpenGraph.MakeGraph(title, type, image, url, description, siteName);

            Assert.Equal("og", graph.Namespaces.First().Value.Prefix);
            Assert.Equal("http://opg.me/ns#", graph.Namespaces.First().Value.SchemaUri.ToString());
            
            Assert.Equal(title, graph.Title);
            Assert.Equal(type, graph.Type);
            Assert.Equal(image, graph.Image.ToString());
            Assert.Equal(url, graph.Url.ToString());
            Assert.Equal(description, graph.Data["og:description"].First().Value);
            Assert.Equal(description, graph["description"].Value);
            Assert.Equal(siteName, graph.Data["og:site_name"].First().Value);
            Assert.Equal(siteName, graph["site_name"]);

            var expected = "<meta property=\"og:title\" content=\"some title\">" +
                "<meta property=\"og:type\" content=\"website\">" +
                "<meta property=\"og:image\" content=\"http://www.go.com/someimg.jpg\">" +
                "<meta property=\"og:url\" content=\"http://www.go.com/\">" +
                "<meta property=\"og:description\" content=\"some description\">" +
                "<meta property=\"og:site_name\" content=\"my site\">";
            Assert.Equal(expected, graph.ToString());
        }

        /// <summary>
        /// Tests parsing the HTML
        /// </summary>
        [Fact]
        public void TestParsingHtmlValidGraphParsingTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.validSampleContent, true);

            Assert.Equal(2, graph.Namespaces.Count);
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://opg.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("product", graph.Namespaces["product"].Prefix);
            Assert.Equal("http://ogp.me/ns/product#", graph.Namespaces["product"].SchemaUri.ToString());

            Assert.Equal("product", graph.Type);
            Assert.Equal("Product Title", graph.Title);
            Assert.Equal("http://www.test.com/test.png", graph.Image.ToString());
            Assert.Equal("http://www.test.com/", graph.Url.ToString());
            Assert.Equal("My Description", graph.Data["og:description"].First().Value);
            Assert.Equal("My Description", graph["description"]);
            Assert.Equal("Test Site", graph.Data["og:site_name"].First().Value);
            Assert.Equal("Test Site", graph["og:site_name"]);
        }

        /// <summary>
        /// Tests parsing the HTML that is missing URLs
        /// </summary>
        [Fact]
        public void TestParsingHtmlHtmlMissingUrlsTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidMissingRequiredUrls);

            Assert.Equal(1, graph.Namespaces.Count);
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://opg.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("product", graph.Type);
            Assert.Equal("Product Title", graph.Title);
            Assert.Null(graph.Image);
            Assert.Null(graph.Url);
            Assert.Equal("My Description", graph.Data["og:description"].First().Value);
            Assert.Equal("My Description", graph["description"]);
            Assert.Equal("Test Site", graph.Data["og:site_name"].First().Value);
            Assert.Equal("Test Site", graph["site_name"]);
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidSampleContent, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification throws an exception
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingMissingAllMetaTest()
        {
            Assert.Throws<InvalidSpecificationException>(() => OpenGraph.ParseHtml(this.invalidMissingAllMeta, true));
        }

        /// <summary>
        /// Test that parsing the HTML with invalid graph specification passes when validate specification boolean is off
        /// </summary>
        [Fact]
        public void TestParsingHtmlInvalidGraphParsingWithoutCheckTest()
        {
            OpenGraph graph = OpenGraph.ParseHtml(this.invalidSampleContent);

            Assert.Equal(string.Empty, graph.Type);
            Assert.False(graph.Data.ContainsKey("mistake"));
            Assert.Equal("Product Title", graph.Title);
            Assert.Equal("http://www.test.com/test.png", graph.Image.ToString());
            Assert.Equal("http://www.test.com/", graph.Url.ToString());
            Assert.Equal("My Description", graph.Data["og:description"].First().Value);
            
            Assert.Equal("Test Site", graph.Data["og:site_name"].First().Value);
        }

        /// <summary>
        /// Tests the parsing amazon URL asynchronous test.
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task TestParsingAsyncTest()
        {
            OpenGraph graph = await OpenGraph.ParseUrlAsync(SpacedLink);
            this.AssertSpaced(graph);
        }

        /// <summary>
        /// Test parsing a URL
        /// </summary>
        [Fact]
        public void TestParsingUrlTest()
        {
            OpenGraph graph = OpenGraph.ParseUrl(SpacedLink);
            this.AssertSpaced(graph);
        }

        /// <summary>
        /// Tests the parsing URL asynchronous validate encoding is correct.
        /// </summary>
        /// <returns>A task</returns>
        [Fact]
        public async Task TestParsingUrlAsyncValidateEncodingIsCorrect()
        {
            var expectedContent = "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = await OpenGraph.ParseUrlAsync("https://vk.com/wall-41600377_66756");
            Assert.Equal(expectedContent, tags.Data["og:description"].First().Value);
        }
        
        /// <summary>
        /// Tests the parsing URL validate encoding is correct.
        /// </summary>
        [Fact]
        public void TestParsingUrlValidateEncodingIsCorrect()
        {
            var expectedContent = "Создайте себе горное настроение с нашим первым фан-китом по игре #SteepGame&amp;#33; -&amp;gt; http://ubi.li/u8w9n";
            var tags = OpenGraph.ParseUrl("https://vk.com/wall-41600377_66756");
            Assert.Equal(expectedContent, tags.Data["og:description"].First().Value);
        }

        /// <summary>
        /// Tests the meta charset parses correctly.
        /// </summary>
        [Fact]
        public void TestMetaCharsetParsesCorrectly()
        {
            var expectedTitle = "Réalité virtuelle : 360° de bonheur à améliorer";
            var expectedDescription = "Le cinéma à 360° a désormais son festival. Organisé par le Forum des images, le premier Paris Virtual Film Festival a donc vu le jour. Narration, réalisation, montage… une révolution balbutiante est en marche. Tour d&#039;horizon.";

            var ogs = OpenGraph.ParseUrl("http://www.telerama.fr/cinema/realite-virtuelle-360-de-bonheur-a-ameliorer,144339.php?utm_medium=Social&utm_source=Twitter&utm_campaign=Echobox&utm_term=Autofeed#link_time=1466595239");

            Assert.Equal(expectedTitle, ogs.Title);
            Assert.Equal(expectedDescription, ogs.Data["og:description"].First().Value);
        }

        [Fact]
        public void TestUrlDecodingUrlValues()
        {
            var expectedUrl = "https://tn.periscope.tv/lXc5gSh6UPaWdc37LtVCb3UdtSfvj2QNutojPK2du5YWrNchfI4wXpwwHKTyfDhmfT2ibsBZV4doQeWlhSvI4A==/chunk_314.jpg?Expires=1781852253&Signature=U5OY3Y2HRb4ETmakQAPwMcv~bqu6KygIxriooa41rk64RcDfjww~qpVgMR-T1iX4S9NxfvXHLMT3pEckBDEOicsNO7oUAo4NieH9GRB2Sv0EA7swxLojD~Zn98ThNWTF5fSzv6SSPjyvctsqBiRmvAN6x7fmMH6l3vzx8ePSCgdEm8-31lUAz7lReBNZQjYSi~C8AwqZVI0Mx6y8lNKklL~m0e6RTGdvr~-KIDewU3wpjSdX7AgpaXXjahk4x-ceUUKcH3T1j--ZjaY7nqPO9fbMZFNPs502A32mrcmaZCzvaD~AuoH~u3y44mJVjzHRrpTxHIBklqHxAgc7dzverg__&Key-Pair-Id=APKAIHCXHHQVRTVSFRWQ";
            var og = OpenGraph.ParseUrl("https://www.periscope.tv/w/1DXxyZZZVykKM");

            Assert.Equal(expectedUrl, og.Image.ToString());
        }

        /// <summary>
        /// Asserts the spaced open graph data.
        /// </summary>
        /// <param name="graph">The graph.</param>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void AssertSpaced(OpenGraph graph)
        {
            Assert.Equal(2, graph.Namespaces.Count);
            Assert.Equal("http://ogp.me/ns#", graph.Namespaces["og"].SchemaUri.ToString());
            Assert.Equal("og", graph.Namespaces["og"].Prefix);
            Assert.Equal("http://www.facebook.com/2008/fbml", graph.Namespaces["fb"].SchemaUri.ToString());
            Assert.Equal("fb", graph.Namespaces["fb"].Prefix);
            
            Assert.Equal(SpacedLink, graph.Url.ToString());
            Assert.StartsWith("Spaced (TV Series 1999–2001)", graph.Title);
            Assert.Contains("Simon", graph.Data["og:description"].First().Value);
            Assert.Contains("imdb.com/images", graph.Image.ToString());
            Assert.Equal("video.tv_show", graph.Type);
            Assert.Equal("IMDb", graph.Data["og:site_name"].First().Value);

            Assert.Equal("115109575169727", graph.Data["fb:app_id"].First().Value);
        }
    }
}
