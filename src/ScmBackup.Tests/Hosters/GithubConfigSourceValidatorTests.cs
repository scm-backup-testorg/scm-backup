﻿using System.Linq;
using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class GithubConfigSourceValidatorTests
    {
        private ConfigSource config;

        public GithubConfigSourceValidatorTests()
        {
            config = new ConfigSource();
            config.Hoster = "github";
            config.Type = "user";
            config.Name = "foo";
            config.AuthName = "authname";
            config.Password = "pass";
        }

        [Fact]
        public void ReturnsErrorWhenHosterNotGithub()
        {
            config.Hoster = "foo";

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        public void ReturnsErrorWhenTypeIsInvalid(string value)
        {
            config.Type = value;

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData("user")]
        [InlineData("org")]
        public void ValidatesWhenTypeIsValid(string value)
        {
            config.Type = value;

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.False(result.Messages.Any());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReturnsErrorWhenNameIsEmpty(string value)
        {
            config.Name = value;

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReturnsWarningWhenAuthNameIsEmpty(string value)
        {
            config.AuthName = value;

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Warn, result.Messages[0].Error);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReturnsWarningWhenPasswordIsEmpty(string value)
        {
            config.Password = value;

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Warn, result.Messages[0].Error);
        }
    }
}
