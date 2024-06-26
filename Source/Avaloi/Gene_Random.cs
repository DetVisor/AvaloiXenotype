﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Avaloi
{
    public class Gene_Random : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            pawn.genes.AddGene(def.GetModExtension<RandomGeneExtension>().genes.RandomElement(), pawn.genes.IsXenogene(this));
            pawn.genes.RemoveGene(this);
        }
    }

    public class RandomGeneExtension : DefModExtension
    {
        public List<GeneDef> genes;
    }
}
