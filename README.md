# NDiagnostics.Monitoring

This library makes it easier for developers to gain valuable application insight. This is achieved by using "meters" to measure key performance indicators during runtime.

The library utilizes [Performance Counters](http://msdn.microsoft.com/en-us/library/windows/desktop/aa373083(v=vs.85).aspx) as its basis if available, but falls back to using transient in-memory counters if not. Therefore, the library can be used even in environments where administrative privilages for the management of Performance Counters are lacking.

The library provides out-of-the-box a more elaborated API than the cumbersome provided by the .NET framework in the [`System.Diagnostics`](http://msdn.microsoft.com/en-us/library/System.Diagnostics(v=vs.110).aspx) namespace. The API allows performance data to be collected and evaluated by using single line statements with minimal pollution of the code. 

## Installation

~~~shell
nuget install NDiagnostics.Monitoring
~~~
or
~~~shell
PM> Install-Package NDiagnostics.Monitoring
~~~

## Usage

TODO

## Meter Types

### Instantaneous Meters

Instant meters measure the most recent absolute values. Any value can be obtained from a single measurement at any point in time.

|**Type**|**`MeterType.InstantValue`**|
|--:|:--|
|Description|measures an integer|
|Example|total number of web requests|
|Substitutes|`PerformanceCounterType.NumberOfItems32`, `PerformanceCounterType.NumberOfItems64`, `PerformanceCounterType.NumberOfItemsHEX32`, `PerformanceCounterType.NumberOfItemsHEX64`|
|Interface|
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
~~~|

|**Type**|**`MeterType.InstantTime`**|
|--:|:--|
|Description|measures a period of time in seconds|
|Example|total elapsed time since application start|
|Substitutes|`PerformanceCounterType.ElapsedTime`|
|Interface|
~~~c#
public interface IInstantTime : IMeter
{
    void Set(TimeStamp start);

    new InstantTimeSample Current { get; }
}
~~~|

|**Type**|**`MeterType.InstantRatio`**|
|--:|:--|
|Description|measures a fraction of integers|
|Example|percentage of free vs. total memory|
|Substitutes|`PerformanceCounterType.RawFraction`|
|Interface|
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
~~~|

### Average Meters

Average meters measure the average of values within a given time frame. Any value can be obtained from two measurements at different points in time.

|**Type**|**`MeterType.AverageValue`**|
|--:|:--|
|Description|measures the average of an integer|
|Example|average queue length|
|Substitutes|`PerformanceCounterType.AverageCount64`|
|Interface|
~~~c#
public interface IAverageValue : IMeter
{
    void Sample(long value);

    new AverageValueSample Current { get; }
}
~~~|

|**Type**|**`MeterType.AverageTime`**|
|--:|:--|
|Description|measures the average of a period of time in seconds|
|Example|average processing time per message|
|Substitutes|`PerformanceCounterType.AverageTimer32`|
|Interface|
~~~c#
public interface IAverageTime : IMeter
{
    void Sample(Time elapsedTime);

    new AverageTimeSample Current { get; }
}
~~~|

|**Type**|**`MeterType.AverageRatio`**|
|--:|:--|
|Description|measures the average of a fraction of integers|
|Example|average percentage of free vs total memory|
|Substitutes|`PerformanceCounterType.SampleFraction`|
|Interface|
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
~~~|

|**Type**|**`MeterType.AverageRate`**|
|--:|:--|
|Description|measures the average rate of items per second|
|Example|average number of messages processed per second|
|Substitutes|`PerformanceCounterType.SampleCounter`, `PerformanceCounterType.RateOfCountsPerSecond32`, `PerformanceCounterType.RateOfCountsPerSecond64`|
|Interface|
~~~c#
public interface IAverageRate : IMeter
{
    void Sample();

    new AverageRateSample Current { get; }
}
~~~|

### Differential Meters 

Differential meters measure the difference between values at the beginning and the end of a given time frame. Any value can be obtained from two measurements at different points in time.

|**Type**|**`MeterType.DifferentialValue`**|
|--:|:--|
|Description|measures the average rate of items per second|
|Example|?|
|Substitutes|`PerformanceCounterType.CounterDelta32`, `PerformanceCounterType.CounterDelta64`|
|Interface|
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
~~~|

### Percentage of Time Meters

Percentage of time meters measure the difference between values at the beginning and the end of a given time frame. Any value can be obtained from two measurements at different points in time.

## Contributions

Just send a pull request. You will be granted commit access if you send quality pull requests.
