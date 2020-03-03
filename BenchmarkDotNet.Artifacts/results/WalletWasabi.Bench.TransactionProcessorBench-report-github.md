``` ini

BenchmarkDotNet=v0.12.0, OS=ubuntu 18.04
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56106, CoreFX 4.700.19.56202), X64 RyuJIT
  Job-XFMRGX : .NET Core 3.1.0 (CoreCLR 4.700.19.56106, CoreFX 4.700.19.56202), X64 RyuJIT
  Job-NAJQOR : .NET Core 3.1.0 (CoreCLR 4.700.19.56106, CoreFX 4.700.19.56202), X64 RyuJIT

Runtime=.NET Core 3.1  

```
|  Method |     Toolchain | TRANSACTIONS |        Mean |       Error |      StdDev | Rank |
|-------- |-------------- |------------- |------------:|------------:|------------:|-----:|
| **Process** |       **Default** |          **100** |    **802.5 us** |    **15.17 us** |    **14.90 us** |    **1** |
| Process | netcoreapp3.1 |          100 |    849.8 us |    16.45 us |    21.39 us |    2 |
| **Process** |       **Default** |         **1000** |  **8,518.6 us** |   **149.98 us** |   **132.95 us** |    **3** |
| Process | netcoreapp3.1 |         1000 |  8,414.3 us |   167.55 us |   156.73 us |    3 |
| **Process** |       **Default** |        **10000** | **84,043.7 us** | **1,629.23 us** | **2,388.10 us** |    **4** |
| Process | netcoreapp3.1 |        10000 | 82,340.1 us | 2,392.46 us | 2,120.85 us |    4 |
