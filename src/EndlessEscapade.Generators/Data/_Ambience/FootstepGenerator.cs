using System.IO;
using System.Text;
using Hjson;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

[Generator(LanguageNames.CSharp)]
public sealed class FootstepGenerator : IIncrementalGenerator
{
    private const string TOOL_VERSION = "0.1";

    /// <summary>
    ///     The file extension associated with this generator.
    /// </summary>
    public const string EXTENSION = ".eefootstep";

    public void Initialize(IncrementalGeneratorInitializationContext initializationContext) {
        var files = initializationContext.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(EXTENSION));

        var contents = files.Select(
            static (text, token) => {
                var content = text.GetText(token);

                var json = HjsonValue.Parse(content.ToString()).ToString(Stringify.Plain);
                var data = JsonConvert.DeserializeObject<FootstepData>(json);

                return (
                    Name: Path.GetFileNameWithoutExtension(text.Path),
                    Data: data
                );
            }
        );

        initializationContext.RegisterSourceOutput(
            contents,
            static (sourceContext, content) => {
                sourceContext.AddSource(
                    $"{content.Name}.g.cs",
                    SourceText.From(GenerateFootstep(content.Name, in content.Data), Encoding.UTF8)
                );
            }
        );
    }

    private static string GenerateFootstep(string name, in FootstepData data) {
        return $@"using Terraria.Audio;

namespace EndlessEscapade.Common.Ambience;

public sealed class {name} : ModFootstep
{{
	public override SoundStyle Sound {{ get; }} = new(""{data.SoundStyleData.SoundPath}"", {data.SoundStyleData.Variants}, SoundType.Ambient) {{
		Volume = {data.SoundStyleData.Volume}f,
        PitchVariance = {data.SoundStyleData.PitchVariance}f,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	}};

	public override string Material {{ get; }} = ""{data.Material}"";
}}";
    }
}
