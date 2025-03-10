using EndlessEscapade.Core.ECS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Core.ECS.Systems;
public readonly struct Rule(Rule.RuleType type, ComponentID operand)
{
    public readonly RuleType Operator = type;
    public readonly ComponentID Operand = operand;

    public bool Applies(byte[] indexMapping) =>
        Operator switch
        {
            RuleType.Include => indexMapping[Operand.GetRawValue()] != byte.MaxValue,
            RuleType.Exclude => indexMapping[Operand.GetRawValue()] == byte.MaxValue,
            _ => throw new NotSupportedException(),
        };

    public enum RuleType
    {
        Include,
        Exclude,
    }
}
