//using Sweaj.Patterns.AI.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

namespace Sweaj.Patterns.AI.Generation
{


    using System.Reflection;

    public static class OrganismFactory
    {
        // Cache the list of properties and fields marked with the Gene attribute in the Organism struct
        private static readonly PropertyInfo[] geneProperties = typeof(Organism).GetProperties().Where(p => p.GetCustomAttribute<GeneAttribute>() != null).ToArray();
        private static readonly FieldInfo[] geneFields = typeof(Organism).GetFields().Where(f => f.GetCustomAttribute<GeneAttribute>() != null).ToArray();

        // Preallocate a fixed number of Organism structs
        private static readonly Organism[] organisms = new Organism[100];
        private static int currentOrganism = 0;

        public static Organism CreateOffspring(Organism parent1, Organism parent2)
        {
            // Reuse an existing Organism struct to avoid allocations
            Organism offspring = organisms[currentOrganism];
            currentOrganism = (currentOrganism + 1) % organisms.Length;

            // Set the value of each gene in the offspring's DNA to the average of the corresponding genes in the parent organisms
            foreach (var geneProperty in geneProperties)
            {
                byte geneValue1 = (byte)geneProperty.GetValue(parent1);
                byte geneValue2 = (byte)geneProperty.GetValue(parent2);
                geneProperty.SetValue(offspring, (byte)((geneValue1 + geneValue2) / 2));
            }
            foreach (var geneField in geneFields)
            {
                byte geneValue1 = (byte)geneField.GetValue(parent1);
                byte geneValue2 = (byte)geneField.GetValue(parent2);
                geneField.SetValue(offspring, (byte)((geneValue1 + geneValue2) / 2));
            }

            // Return the offspring
            return offspring;
        }
    }
}