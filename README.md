# NDiagnostics.Metering

This library makes it easier for developers to gain valuable application insight. This is achieved by using "meters" 
to measure key performance indicators during runtime.

The library utilizes [performance counters](http://msdn.microsoft.com/en-us/library/windows/desktop/aa373083(v=vs.85).aspx) 
as its basis if available, but falls back to using transient in-memory counters if not. Therefore, the library can 
be used even in environments where administrative privilages for the management of performance counters are lacking.

The library provides out-of-the-box a more elaborated API than the cumbersome provided by the .NET framework in the 
[`System.Diagnostics`](http://msdn.microsoft.com/en-us/library/System.Diagnostics(v=vs.110).aspx) namespace. The API 
allows performance data to be collected and evaluated by using single line statements with minimal pollution of the 
code. 

# Installation

~~~shell
nuget install NDiagnostics.Metering
~~~
or
~~~shell
PM> Install-Package NDiagnostics.Metering
~~~

# Basics

Meters have a type that essentially defines how the values they represent have to be interpreted. Some of the 
meter types (so called [Instantaneous Meters](##markdown-header-instananeous-meters)) will require only a single 
sample, all others require two samples in order to calculate their corresponding value. Detailed specifications 
of the various meter types can be found in section [Meter Types](##markdown-header-meter-types).
 
Meters are bundled together in logical units, so called meter categories. Meter categories come in two flavors: 
single-instance and multi-instance. A single-instance category has only one global value for each of its meters. 
A good example of a category is the `System` category of Windows. It has meters like `System Up Time` and 
`Threads`, and clearly there is only one global value for each of the its meters.  

Multi-instance categories on the other hand can have an unlimited number of different values for each meter. 
A good example is `Process`, which has one instance for each process running, and therefore a set of instance 
values for each of its meters. Each instance of a meter can be uniquely identified by its unique instance name 
(e.g. process name). Typically, but not necessarily, there is a global instance that is commonly named `_Total`, 
that represents the aggregate value of all other instance values.

All meter instances have a lifetime that is either tied to the lifetime of their meter category (global lifetime) 
or to the lifetime of a particular process (process lifeftime). All meters of a single-instance category 
have a global lifetime by default. Multi-instance meters on the other hand can either have a global lifetime 
(i.e. they are created whenever the meter category is created) or a process lifetime. Instances with a process 
lifetime are created on the fly at runtime and their lifetime ends automatically whenever the creating process 
is terminated. A good example of a category with such instances is the `Process` category of Windows.

# Usage

## Declaration

~~~c#
[MeterCategory("System", "The System category consists of meters that apply to the system as a whole.", MeterCategoryType.SingleInstance)]
public enum SystemMeterCategory
{
    [Meter("System Up Time", "System Up Time is the elapsed time (in seconds) that the computer has been running since it was last started.", MeterType.InstantTime)]
    SystemUpTime,

    [Meter("Threads", "Threads is the number of threads at the time of the sample.", MeterType.InstantValue)]
    Threads,
}
~~~
or
~~~c#
[MeterCategory("Process", "The Process category consists of meters that monitor running application programs and system processes.", MeterCategoryType.MultiInstance)]
public enum ProcessMeterCategory
{
    [Meter("% Processor Time", "% Processor Time is the percentage of elapsed time that the process threads spent executing processor instructions.", MeterType.Timer100Ns)]
    ProcessorTime,

    [Meter("Thread Count", "Thread Count is the number of threads currently active in this process.", MeterType.InstantValue)]
    ThreadCount,
}
~~~

## Install

~~~c#
MeterCategory.Install<SystemMeterCategory>();
~~~
or
~~~c#
MeterCategory.Install<ProcessMeterCategory>();
~~~

## Uninstall

~~~c#
MeterCategory.Uninstall<SystemMeterCategory>();
~~~
or
~~~c#
MeterCategory.Uninstall<ProcessMeterCategory>();
~~~

## Create Category

~~~c#
var systemCategory = MeterCategory.Create<SystemMeterCategory>();
~~~
or
~~~c#
var processCategory = MeterCategory.Create<ProcessMeterCategory>();
processCategory.CreateInstance(MultiInstance.DefaultInstanceName);
processCategory.CreateInstance(processName, InstanceLifetime.Process);
~~~

## Create Meter

~~~c#
var systemUpTime = systemCategory[SystemMeterCategory.SystemUpTime].As<IInstantTime>();
~~~
or
~~~c#
var threadCountGlobal = processCategory[ProcessMeterCategory.ThreadCount, MultiInstance.DefaultInstanceName].As<IInstantValue>();
var threadCountInstance = processCategory[ProcessMeterCategory.ThreadCount, processName].As<IInstantValue>();
~~~

## Write Meter Data

~~~c#
systemUpTime.Set(TimeStmap.Now);
~~~
or
~~~c#
threadCountGlobal.Increment();
threadCountInstance.Increment();
~~~

## Read Meter Data

~~~c#
float systemUpTimeInSeconds = systemUpTime.Sample().Value();
~~~
or
~~~c#
long totalThreadCount = threadCountGlobal.Sample().Value();
long processThreadCount = threadCountInstance.Sample().Value();
~~~

# Meter Types

~~~c#
public enum MeterType : long
{
    // Instantaneous Meters
    InstantValue,
    InstantTime,
    InstantPercentage,
    // Average Meters
    AverageValue,
    AverageTime,
    // Sample Meters
    SampleRate,
    SamplePercentage,
    // Differential Meters
    DifferentialValue,
    // Percentage of Time Meters
    Timer,
    TimerInverse,
    MultiTimer,
    MultiTimerInverse,
    Timer100Ns,
    Timer100NsInverse,
    MultiTimer100Ns,
    MultiTimer100NsInverse,
}
~~~

## Instantaneous Meters
Instant meters measure the most recent absolute values. Any value can be obtained from a single measurement at any point in time.

### InstantValue
#### Description
Measures an integer. Substitutes performance counters of type `NumberOfItems32`, `NumberOfItems64`, `NumberOfItemsHEX32`, `NumberOfItemsHEX64`.
#### Interface 
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

### InstantPercentage
#### Description
Measures a percentage as a fraction of integers. Substitutes performance counters of type `RawFraction`.
#### Interface 
~~~c#
public interface IInstantPercentage : IMeter
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

    new InstantPercentageSample Current { get; }
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

## Sample Meters 
Sample meters measure the occurrence of events per time frame. Any value can be obtained from two measurements at different points in time.

### SampleRate
#### Description
Measures the rate of occurrences of events per time frame. Substitutes performance counters of type `SampleCounter`, `RateOfCountsPerSecond32`, `RateOfCountsPerSecond64`.
#### Interface 
~~~c#
public interface ISampleRate : IMeter
{
    void Sample();

    new SampleRateSample Current { get; }
}
~~~
#### Example
Number of messages processed per time frame.

### SamplePercentage
#### Description
Measures the percentage of occurrences of type A events (e.g. success) vs. type B events (e.g. failure) per time frame. Substitutes performance counters of type `SampleFraction`.
#### Interface 
~~~c#
public interface ISamplePercentage : IMeter
{
    void SampleA();

    void SampleB();

    new SamplePercentageSample Current { get; }
}
~~~
#### Example
Percentage of free vs total memory per time frame.

## Differential Meters 
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

## Percentage of Time Meters
Percentage of time meters measure the difference between values at the beginning and the end of a given time frame. Any value can be obtained from two measurements at different points in time.

### Timer
#### Description
Measures the percentage of elapsed time of activity. Uses `Stopwatch.ElapsedTicks`. Substitutes performance counters of type `CounterTimer`.
#### Interface 
~~~c#
public interface ITimer : IMeter
{
    void Sample(Time elapsedTimeOfActivity);

    new TimerSample Current { get; }
}
~~~
#### Example
Percentage of elapsed time that a method has spent executing.

### TimerInverse
#### Description
Measures the percentage of elapsed time of inactivity. Uses `Stopwatch.ElapsedTicks`. Substitutes performance counters of type `CounterTimerInverse`.
#### Interface 
~~~c#
public interface ITimerInverse : IMeter
{
    void Sample(Time elapsedTimeOfInactivity);

    new TimerInverseSample Current { get; }
}
~~~
#### Example
Percentage of idle time that a method has spent waiting.

### Timer100Ns
#### Description
Measures the percentage of elapsed time of activity. Uses high precision `DateTime.Ticks`. Substitutes performance counters of type `Timer100Ns`.
#### Interface 
~~~c#
public interface ITimer100Ns : IMeter
{
    void Sample(Time100Ns elapsedTimeOfActivity);

    new Timer100NsSample Current { get; }
}
~~~
#### Example
Percentage of elapsed time for a processor executing instructions in user mode.

### Timer100NsInverse
#### Description
Measures the percentage of elapsed time of inactivity. Uses high precision `DateTime.Ticks`. Substitutes performance counters of type `Timer100NsInverse`.
#### Interface 
~~~c#
public interface ITimer100NsInverse : IMeter
{
    void Sample(Time100Ns elapsedTimeOfInactivity);

    new Timer100NsInverseSample Current { get; }
}
~~~
#### Example
Percentage of idle time for a processor.

## Contributions

Just send a pull request. You will be granted commit access if you send quality pull requests.
