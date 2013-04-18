using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackHoleLoans.PlayerRelated;

namespace BlackHoleLoans
{
  [Flags]
  public enum Skills { Fire, Ice, Heal }//add skill name
  public class Skill
  {
    public float skillRatio { get; set; }
    public string Name { get; set; }
    public bool isDamage;
    public bool isHealing;
    public Skill(Skills skill)//add new if statement for new skill
    {
      isDamage = false;
      isHealing = false;

      if (skill.HasFlag(Skills.Fire))
      {
        skillRatio = 2.0f;
        Name = "Fire";
        isDamage = true;
        isHealing = false;
      }
      if (skill.HasFlag(Skills.Ice))
      {
        skillRatio = 1.5f;
        Name = "Ice";
        isDamage = true;
        isHealing = false;
      }
      if (skill.HasFlag(Skills.Heal))
      {
        skillRatio = 1.0f;
        Name = "Heal";
        isDamage = false;
        isHealing = true;
      }
    }
  }
}
