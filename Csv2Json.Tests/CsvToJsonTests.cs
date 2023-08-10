using FluentAssertions.Json;
using Newtonsoft.Json.Linq;

namespace Csv2Json.Tests;

public class CsvToJsonTests
{
    [Fact]
    public void Foo()
    {
        string csv = """
             foo
             bar
             """;
        var expectedJson = JToken.Parse(""" {"foo": "bar"} """);
        var converter = new CsvToJsonConverter();

        var actualJson = JToken.Parse(converter.Convert(csv));

        actualJson.Should().BeEquivalentTo(expectedJson);
    }
}