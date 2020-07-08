using System.Collections.Generic;
using UnityEngine;
using BAC;

public class CardScript_10000 : CardScript
{
    public CardScript_10000()
    {
        ID = 10000;
        Name = "普通攻击";
        Desc = "进行一次普通攻击";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10010 : CardScript
{
    public CardScript_10010()
    {
        ID = 10010;
        Name = "迅雷斩";
        Desc = "造成80%伤害。";
        Cost = 0;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10020 : CardScript
{
    public CardScript_10020()
    {
        ID = 10020;
        Name = "拔刀斩";
        Desc = "造成200%伤害。放逐。";
        Cost = 0;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        ExhaustAfterUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10030 : ComboCardScript
{
    public CardScript_10030()
    {
        ID = 10030;
        Name = "无量斩";
        Desc = "造成200%伤害。连击(2)：抽1张牌。";
        Cost = 2;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 2;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
        CallComboEffect(delegate ()
        {
            AM.AddActionOnBottom(new DrawCardAction(1));
        });
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new DrawCardAction(1));
        // }
    }
}

public class CardScript_10040 : CardScript
{
    public CardScript_10040()
    {
        ID = 10040;
        Name = "烈风斩";
        Desc = "造成100%伤害。本回合每使用一张卡牌，伤害提高30%。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10050 : ComboCardScript
{
    public CardScript_10050()
    {
        ID = 10050;
        Name = "盈元斩";
        Desc = "造成100%伤害。连击(2)：则获得1点能量。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 2;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
        CallComboEffect(delegate ()
        {
            AM.AddActionOnBottom(new GainEnergyAction(1));
        });
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new GainEnergyAction(1));
        // }
    }
}

public class CardScript_10060 : ComboCardScript
{
    public CardScript_10060()
    {
        ID = 10060;
        Name = "修罗斩";
        Desc = "造成300%伤害。连击(2)：消耗-2。";
        Cost = 3;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 2;
    }

    public override void OnJoinHnad()
    {
        // if (CM.UsedCardCount >= 2)
        // {
        //     this.cost = 1;
        // }
        // else
        // {
        //     this.cost = 3;
        // }
        CallComboEffect(
            delegate ()
            {
                this.Cost = 1;
            },
            delegate ()
            {
                this.Cost = 3;
            }
        );
        this.owner.RefreshUI();
    }
    public override void OnOtherCardUse() => OnJoinHnad();

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10070 : CardScript
{
    public CardScript_10070()
    {
        ID = 10070;
        Name = "疾风连刺";
        Desc = "造成两次80%伤害。";
        Cost = 3;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10080 : CardScript
{
    private float proveRate = 0.3f;
    public CardScript_10080()
    {
        ID = 10080;
        Name = "万剑归宗";
        Desc = "对敌人造成100%伤害。本局战斗中每使用过一张「拔刀斩」，伤害提高30%。\n当前提升：0%";
        Cost = 2;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        Param = new List<int>() { 10020 };
    }

    public override void OnJoinHnad()
    {
        int count = 0;
        foreach (CardScript item in CM.UsedHistory)
        {
            if (item.ID == Param[0])
            {
                count++;
            }
        }
        Desc = "对敌人造成100%伤害。本局战斗中每使用过一张「拔刀斩」，伤害提高30%。\n当前提升：" + count * proveRate * 100 + " %";
        this.owner.RefreshUI();
    }
    public override void OnOtherCardUse() => OnJoinHnad();
    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        int count = 0;
        foreach (CardScript item in CM.UsedHistory)
        {
            if (item.ID == Param[0])
            {
                count++;
            }
        }
        int attack = Mathf.CeilToInt((1 + count * proveRate) * PlayerUnit.Attack);
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, attack));
    }
}

public class CardScript_10090 : ComboCardScript
{
    public CardScript_10090()
    {
        ID = 10090;
        Name = "夺命刺击";
        Desc = "造成100%伤害。连击(2)：此伤害必定暴击。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 2;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        CallComboEffect(
            delegate ()
            {
                AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack, 100));
            },
            delegate ()
            {
                AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
            }
        );
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new AttackAction(BM.playerGO, target, playerUnit.attack, 100));
        // }
        // else
        // {
        //     AM.AddActionOnBottom(new AttackAction(BM.playerGO, target, playerUnit.attack));
        // }
    }
}

public class CardScript_10100 : ComboCardScript
{
    public CardScript_10100()
    {
        ID = 10100;
        Name = "弱点突袭";
        Desc = "造成100%伤害。连击(2)：对目标添加一层易伤。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 2;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        CallComboEffect(delegate()
        {
            AM.AddActionOnBottom(new AddBuffAction(target, new Buff_10000(target.GetComponent<BattleUnitContainer>().BattleUnit, 1)));
        });
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new AddBuffAction(target, new Buff_10000(target.GetComponent<BattleUnitContainer>().battleUnit, 1)));
        // }
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

/// <summary>
/// 等待测试
/// </summary>
public class CardScript_10110 : CardScript
{
    public CardScript_10110()
    {
        ID = 10110;
        Name = "故技重施";
        Desc = "造成100%伤害。从弃牌堆中选择一张卡牌加入手牌。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AddBuffAction(target, new Buff_10000(target.GetComponent<BattleUnitContainer>().BattleUnit, 1)));
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
    }
}

public class CardScript_10120 : ComboCardScript
{
    public CardScript_10120()
    {
        ID = 10120;
        Name = "纵横斩";
        Desc = "造成80%伤害。连击(1)：额外造成一次伤害。";
        Cost = 1;
        Type = CardType.Attack;
        TargetType = TargetType.Enemy;
        CanUse = true;
        requireCombo = 1;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
        CallComboEffect(delegate()
        {
            AM.AddActionOnBottom(new AttackAction(BM.PlayerGO, target, PlayerUnit.Attack));
        });
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new AttackAction(BM.playerGO, target, playerUnit.attack));
        // }
    }
}

public class CardScript_20000 : CardScript
{
    public CardScript_20000()
    {
        ID = 20000;
        Name = "护盾";
        Desc = "为自己添加护盾";
        Cost = 1;
        Type = CardType.Defend;
        TargetType = TargetType.None;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = BM.PlayerGO;
        AM.AddActionOnBottom(new GetBarrierAction(target, 10));
    }
}

public class CardScript_30000 : CardScript
{
    public CardScript_30000()
    {
        ID = 30000;
        Name = "智慧祝福";
        Desc = "弃一张牌，抽两张牌";
        Cost = 1;
        Type = CardType.Magic;
        TargetType = TargetType.Card;
        CanUse = true;
    }

    public override void Effect()
    {
        GameObject target = this.myFSM.GetFsmGameObject("Target").Value;
        AM.AddActionOnBottom(new FoldHandCardAction(target));
        AM.AddActionOnBottom(new DrawCardAction(2));
    }
}

public class CardScript_30010 : CardScript
{
    public CardScript_30010()
    {
        ID = 30010;
        Name = "收刀入鞘";
        Desc = "将一张「拔刀斩」加入手牌";
        Cost = 1;
        Type = CardType.Magic;
        TargetType = TargetType.None;
        CanUse = true;
        Param = new List<int>() { 10020 };
    }

    public override void Effect()
    {
        AM.AddActionOnBottom(new GenerateCardAction(Param[0], PileName.HandPile));
    }
}

public class CardScript_30020 : CardScript
{
    public CardScript_30020()
    {
        ID = 30020;
        Name = "移形换影";
        Desc = "抽1张牌，选择一张手牌移入牌库顶。";
        Cost = 1;
        Type = CardType.Magic;
        TargetType = TargetType.None;
        CanUse = true;
    }

    public override void Effect()
    {
        AM.AddActionOnBottom(new DrawCardAction(1));
        ChooseTargetAction action = new ChooseCardTargetAction();
        AM.AddActionOnBottom(action);
        AM.AddActionOnBottom(new MoveCardToDrawPile(action, 0));
    }
}

public class CardScript_30030 : ComboCardScript
{
    public CardScript_30030()
    {
        ID = 30030;
        Name = "行云流水";
        Desc = "抽1张牌。连击(2)：额外抽一张牌。";
        Cost = 1;
        Type = CardType.Magic;
        TargetType = TargetType.None;
        CanUse = true;
        requireCombo = 2;
    }

    public override void Effect()
    {
        AM.AddActionOnBottom(new DrawCardAction(1));
        CallComboEffect(delegate()
        {
            AM.AddActionOnBottom(new DrawCardAction(1));
        });
        // if (CardManager._instance.UsedCardCount >= 2)
        // {
        //     AM.AddActionOnBottom(new DrawCardAction(1));
        // }
    }
}

public class CardScript_40000 : CardScript
{
    public CardScript_40000()
    {
        ID = 40000;
        Name = "小圆盾";
        Desc = "这是一件装备";
        Cost = 1;
        Type = CardType.Equip;
        TargetType = TargetType.None;
        CanUse = true;
    }
}

public class CardScript_50000 : CardScript
{
    public CardScript_50000()
    {
        ID = 50000;
        Name = "诅咒";
        Desc = "无法打出";
        Cost = 1;
        Type = CardType.Evil;
        TargetType = TargetType.None;
        CanUse = false;
    }
}