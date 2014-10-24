using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDiagnostics.Metering.Types;

namespace NDiagnostics.Metering.Test
{
    [TestClass]
    public class TypesFixture
    {
        [TestMethod]
        public void CanCreateTimeStamp100NsFromDateTime()
        {
            // Act
            var timeStamp = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));

            // Assert 
            var result = DateTime.FromFileTimeUtc(timeStamp.Ticks);
            result.Year.Should().Be(2014);
            result.Month.Should().Be(10);
            result.Day.Should().Be(3);
            result.Hour.Should().Be(12);
            result.Minute.Should().Be(30);
            result.Second.Should().Be(15);
        }

        [TestMethod]
        public void CanCreateTimeStamp100NsNow()
        {
            // Arrange
            var dateTimeNow = DateTime.UtcNow;

            // Act
            var timeStampNow = TimeStamp100Ns.Now;

            // Assert
            var result = DateTime.FromFileTimeUtc(timeStampNow.Ticks);
            result.Year.Should().Be(dateTimeNow.Year);
            result.Month.Should().Be(dateTimeNow.Month);
            result.Day.Should().Be(dateTimeNow.Day);
            result.Hour.Should().Be(dateTimeNow.Hour);
            result.Minute.Should().Be(dateTimeNow.Minute);
            result.Second.Should().Be(dateTimeNow.Second);
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForEqual()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 == timeStamp1;
            var eq22 = timeStamp2 == timeStamp2;
            var eq33 = timeStamp3 == timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 == timeStamp2;
            var eq23 = timeStamp2 == timeStamp3;
            var eq13 = timeStamp1 == timeStamp3;
            var eq21 = timeStamp2 == timeStamp1;
            var eq32 = timeStamp3 == timeStamp2;
            var eq31 = timeStamp3 == timeStamp1;

            // Assert
            eq11.Should().BeTrue();
            eq22.Should().BeTrue();
            eq33.Should().BeTrue();
            eq12.Should().BeTrue();
            eq13.Should().BeFalse();
            eq23.Should().BeFalse();
            eq21.Should().BeTrue();
            eq31.Should().BeFalse();
            eq32.Should().BeFalse();
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForNotEqual()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 != timeStamp1;
            var eq22 = timeStamp2 != timeStamp2;
            var eq33 = timeStamp3 != timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 != timeStamp2;
            var eq23 = timeStamp2 != timeStamp3;
            var eq13 = timeStamp1 != timeStamp3;
            var eq21 = timeStamp2 != timeStamp1;
            var eq32 = timeStamp3 != timeStamp2;
            var eq31 = timeStamp3 != timeStamp1;

            // Assert
            eq11.Should().BeFalse();
            eq22.Should().BeFalse();
            eq33.Should().BeFalse();
            eq12.Should().BeFalse();
            eq13.Should().BeTrue();
            eq23.Should().BeTrue();
            eq21.Should().BeFalse();
            eq31.Should().BeTrue();
            eq32.Should().BeTrue();
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForGreater()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 > timeStamp1;
            var eq22 = timeStamp2 > timeStamp2;
            var eq33 = timeStamp3 > timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 > timeStamp2;
            var eq23 = timeStamp2 > timeStamp3;
            var eq13 = timeStamp1 > timeStamp3;
            var eq21 = timeStamp2 > timeStamp1;
            var eq32 = timeStamp3 > timeStamp2;
            var eq31 = timeStamp3 > timeStamp1;

            // Assert
            eq11.Should().BeFalse();
            eq22.Should().BeFalse();
            eq33.Should().BeFalse();
            eq12.Should().BeFalse();
            eq13.Should().BeFalse();
            eq23.Should().BeFalse();
            eq21.Should().BeFalse();
            eq31.Should().BeTrue();
            eq32.Should().BeTrue();
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForGreaterOrEqual()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 >= timeStamp1;
            var eq22 = timeStamp2 >= timeStamp2;
            var eq33 = timeStamp3 >= timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 >= timeStamp2;
            var eq23 = timeStamp2 >= timeStamp3;
            var eq13 = timeStamp1 >= timeStamp3;
            var eq21 = timeStamp2 >= timeStamp1;
            var eq32 = timeStamp3 >= timeStamp2;
            var eq31 = timeStamp3 >= timeStamp1;

            // Assert
            eq11.Should().BeTrue();
            eq22.Should().BeTrue();
            eq33.Should().BeTrue();
            eq12.Should().BeTrue();
            eq13.Should().BeFalse();
            eq23.Should().BeFalse();
            eq21.Should().BeTrue();
            eq31.Should().BeTrue();
            eq32.Should().BeTrue();
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForSmaller()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 < timeStamp1;
            var eq22 = timeStamp2 < timeStamp2;
            var eq33 = timeStamp3 < timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 < timeStamp2;
            var eq23 = timeStamp2 < timeStamp3;
            var eq13 = timeStamp1 < timeStamp3;
            var eq21 = timeStamp2 < timeStamp1;
            var eq32 = timeStamp3 < timeStamp2;
            var eq31 = timeStamp3 < timeStamp1;

            // Assert
            eq11.Should().BeFalse();
            eq22.Should().BeFalse();
            eq33.Should().BeFalse();
            eq12.Should().BeFalse();
            eq13.Should().BeTrue();
            eq23.Should().BeTrue();
            eq21.Should().BeFalse();
            eq31.Should().BeFalse();
            eq32.Should().BeFalse();
        }

        [TestMethod]
        public void CanCompareTimeStamp100NsForSmallerOrEqual()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            // ReSharper disable EqualExpressionComparison
            var eq11 = timeStamp1 <= timeStamp1;
            var eq22 = timeStamp2 <= timeStamp2;
            var eq33 = timeStamp3 <= timeStamp3;
            // ReSharper restore EqualExpressionComparison
            var eq12 = timeStamp1 <= timeStamp2;
            var eq23 = timeStamp2 <= timeStamp3;
            var eq13 = timeStamp1 <= timeStamp3;
            var eq21 = timeStamp2 <= timeStamp1;
            var eq32 = timeStamp3 <= timeStamp2;
            var eq31 = timeStamp3 <= timeStamp1;

            // Assert
            eq11.Should().BeTrue();
            eq22.Should().BeTrue();
            eq33.Should().BeTrue();
            eq12.Should().BeTrue();
            eq13.Should().BeTrue();
            eq23.Should().BeTrue();
            eq21.Should().BeTrue();
            eq31.Should().BeFalse();
            eq32.Should().BeFalse();
        }

        [TestMethod]
        public void CanSubtractTwoTimeStamp100Ns()
        {
            // Arrange
            var timeStamp1 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp2 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var timeStamp3 = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 15, 18, 00, 00, DateTimeKind.Utc));

            // Act
            var time11 = timeStamp1 - timeStamp1;
            var time22 = timeStamp2 - timeStamp2;
            var time33 = timeStamp3 - timeStamp3;
            var time12 = timeStamp1 - timeStamp2;
            var time23 = timeStamp2 - timeStamp3;
            var time13 = timeStamp1 - timeStamp3;
            var time21 = timeStamp2 - timeStamp1;
            var time31 = timeStamp3 - timeStamp1;
            var time32 = timeStamp3 - timeStamp2;

            // Assert
            var seconds = (float) new TimeSpan(12, 5, 29, 45).TotalSeconds;
            time11.Seconds.Should().Be(0.0F);
            time22.Seconds.Should().Be(0.0F);
            time33.Seconds.Should().Be(0.0F);
            time12.Seconds.Should().Be(0.0F);
            time23.Seconds.Should().Be(-seconds);
            time13.Seconds.Should().Be(-seconds);
            time21.Seconds.Should().Be(0.0F);
            time31.Seconds.Should().Be(seconds);
            time32.Seconds.Should().Be(seconds);
        }

        [TestMethod]
        public void CanSubtractTimeStamp100NsAndTime100Ns()
        {
            // Arrange
            var timeStamp = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var time1 = Time100Ns.FromSeconds(0.0F);
            var time2 = Time100Ns.FromSeconds(3600.0F);
            var time3 = Time100Ns.FromSeconds(-3600.0F);

            // Act
            var timeStamp1 = timeStamp - time1;
            var timeStamp2 = timeStamp - time2;
            var timeStamp3 = timeStamp - time3;

            // Assert
            timeStamp1.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc)).Ticks);
            timeStamp2.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 11, 30, 15, DateTimeKind.Utc)).Ticks);
            timeStamp3.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 13, 30, 15, DateTimeKind.Utc)).Ticks);
        }


        [TestMethod]
        public void CanAddTimeStamp100NsAndTime100Ns()
        {
            // Arrange
            var timeStamp = TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc));
            var time1 = Time100Ns.FromSeconds(0.0F);
            var time2 = Time100Ns.FromSeconds(3600.0F);
            var time3 = Time100Ns.FromSeconds(-3600.0F);

            // Act
            var timeStamp1 = timeStamp + time1;
            var timeStamp2 = timeStamp + time2;
            var timeStamp3 = timeStamp + time3;

            // Assert
            timeStamp1.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 12, 30, 15, DateTimeKind.Utc)).Ticks);
            timeStamp2.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 13, 30, 15, DateTimeKind.Utc)).Ticks);
            timeStamp3.Ticks.Should().Be(TimeStamp100Ns.FromDateTime(new DateTime(2014, 10, 3, 11, 30, 15, DateTimeKind.Utc)).Ticks);
        }
    }
}
