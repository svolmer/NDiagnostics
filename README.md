# NDiagnostics.Monitoring

This library makes it easier for developers to gain valuable application insight. This is achieved by using "meters" to measure key performance indicators during runtime.

The library utilizes [Performance Counters](http://msdn.microsoft.com/en-us/library/windows/desktop/aa373083(v=vs.85).aspx) as its basis if available, but falls back to using transient in-memory counters if not. Therefore, the library can be used even in environments where administrative privilages for the management of Performance Counters are lacking.

The library provides out-of-the-box a more elaborated API than the cumbersome provided by the .NET framework in the [`System.Diagnostics`](http://msdn.microsoft.com/en-us/library/System.Diagnostics(v=vs.110).aspx) namespace. The API allows performance data to be collected and evaluated by using single line statements with minimal pollution of the code. 

# Installation

~~~shell
nuget install NDiagnostics.Monitoring
~~~
or
~~~shell
PM> Install-Package NDiagnostics.Monitoring
~~~

# Meter Types

## Instantaneous Meters
Instant meters measure the most recent absolute values. Any value can be obtained from a single measurement at any point in time.

### InstantValue
#### Description
Measures an integer. Substitutes performance counters of type `NumberOfItems32`, `NumberOfItems64`, `NumberOfItemsHEX32`, `NumberOfItemsHEX64`.
#### interface 
~~~c#
public interface IInstantValue : IMeter
{
    long Increment();

    long IncrementBy(long value);

    long Decrement();

    long DecrementBy(long value);

    void Set(long value);

    new InstantValueSample Current { get; }
}
~~~
#### Example
Total number of web requests.

### InstantTime
#### Description
Measures a period of time in seconds. Substitutes performance counters of type `ElapsedTime`.
#### Interface 
~~~c#
public interface IInstantTime : IMeter
{
    void Set(TimeStamp start);

    new InstantTimeSample Current { get; }
}
~~~
#### Example
Total elapsed time since application start.

### InstantRatio
#### Description
Measures a fraction of integers. Substitutes performance counters of type `RawFraction`.
#### Interface 
~~~c#
public interface IInstantRatio : IMeter
{
    long IncrementNumerator();

    long IncrementNumeratorBy(long value);

    long DecrementNumerator();

    long DecrementNumeratorBy(long value);

    void SetNumerator(long value);

    long IncrementDenominator();

    long IncrementDenominatorBy(long value);

    long DecrementDenominator();

    long DecrementDenominatorBy(long value);

    void SetDenominator(long value);

    new InstantRatioSample Current { get; }
}
~~~
#### Example
Percentage of free vs. total memory.

## Average Meters

Average meters measure the average of values within a given time frame. Any value can be obtained from two measurements at different points in time.

### AverageValue
#### Description
Measures the average of an integer. Substitutes performance counters of type `AverageCount64`.
#### Interface 
~~~c#
public interface IAverageValue : IMeter
{
    void Sample(long value);

    new AverageValueSample Current { get; }
}
~~~
#### Example
Average queue length.

### AverageTime
#### Description
Measures the average of a period of time in seconds. Substitutes performance counters of type `AverageTimer32`.
#### Interface 
~~~c#
public interface IAverageTime : IMeter
{
    void Sample(Time elapsedTime);

    new AverageTimeSample Current { get; }
}
~~~
#### Example
Average processing time per message.

### AverageRatio
#### Description
Measures the the average of a fraction of integers. Substitutes performance counters of type `SampleFraction`.
#### Interface 
~~~c#
public interface IAverageRatio : IMeter
{
    long IncrementNumerator();

    long IncrementNumeratorBy(long value);

    long DecrementNumerator();

    long DecrementNumeratorBy(long value);

    void SetNumerator(long value);

    long IncrementDenominator();

    long IncrementDenominatorBy(long value);

    long DecrementDenominator();

    long DecrementDenominatorBy(long value);

    void SetDenominator(long value);

    new AverageRatioSample Current { get; }
}
~~~
#### Example
Average percentage of free vs total memory.

### AverageRate
#### Description
Measures the average rate of items per second. Substitutes performance counters of type `SampleCounter`, `RateOfCountsPerSecond32`, `RateOfCountsPerSecond64`.
#### Interface 
~~~c#
public interface IAverageRate : IMeter
{
    void Sample();

    new AverageRateSample Current { get; }
}
~~~
#### Example
Average number of messages processed per second.

### Differential Meters 
Differential meters measure the difference between values at the beginning and the end of a given time frame. Any value can be obtained from two measurements at different points in time.

### DifferentialValue
#### Description
Measures the average rate of items per second. Substitutes performance counters of type `CounterDelta32`, `CounterDelta64`.
#### Interface 
~~~c#
public interface IDifferentialValue : IMeter
{
    long Increment();

    long IncrementBy(long value);

    long Decrement();

    long DecrementBy(long value);

    void Set(long value);

    new DifferentialValueSample Current { get; }
}
~~~
#### Example
?

### Percentage of Time Meters
Percentage of time meters measure the difference between values at the beginning and the end of a given time frame. Any value can be obtained from two measurements at different points in time.

## Contributions

Just send a pull request. You will be granted commit access if you send quality pull requests.
