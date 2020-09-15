# ODataComplexTypeIssueSample
A sample project displaying an issue with complex types in odata serializers. Original sample found here: https://github.com/OData/ODataSamples/tree/master/RESTier/Trippin

Run application and run the following odata queries to see the issues:

http://localhost:18384/Events (Should work as expected)

http://localhost:18384/Events?$select=occursAt Throws error at line 51 of CustomODataSerializerProvider.
http://localhost:18384/Events?$select=occursAt,testTime Throws error at line 47 of CustomODataSerializerProvider.

