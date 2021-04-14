# ExposeCppApi
Expose C++ to API using C# dotnet core server - demo

There is 3 projects

The native DLL demo that expose in Add(int, int)

The API server that accept

> http://localhost:5000/Native/1/2
 
and returns

> {
  "a": 1,
  "b": 2,
  "result": 3
}

