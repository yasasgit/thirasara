using System;
using System.Collections.Generic;
using Accord.MachineLearning.Rules;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;

public class MathDotNet
{
    [Obsolete]
    public void PerformLinearRegression()
    {
        // We will try to model a plane as an equation in the form
        // "ax + by + c = z". We have two input variables (x and y)
        // and we will be trying to find two parameters a and b and 
        // an intercept term c.

        // We will use Ordinary Least Squares to create a
        // linear regression model with an intercept term
        var ols = new OrdinaryLeastSquares()
        {
            UseIntercept = true
        };

        // Now suppose you have some points
        double[][] inputs =
        {
    new double[] { 1, 1 },
    new double[] { 0, 1 },
    new double[] { 1, 0 },
    new double[] { 0, 0 },
};

        // located in the same Z (z = 1)
        double[] outputs = { 1, 1, 1, 1 };

        // Use Ordinary Least Squares to estimate a regression model
        MultipleLinearRegression regression = ols.Learn(inputs, outputs);

        // As result, we will be given the following:
        double a = regression.Weights[0]; // a = 0
        double b = regression.Weights[1]; // b = 0
        double c = regression.Intercept;  // c = 1

        // This is the plane described by the equation
        // ax + by + c = z => 0x + 0y + 1 = z => 1 = z.

        // We can compute the predicted points using
        double[] predicted = regression.Transform(inputs);

        // And the squared error loss using 
        double error = new SquareLoss(outputs).Loss(predicted);

        // We can also compute other measures, such as the coefficient of determination r²
        double r2 = new RSquaredLoss(numberOfInputs: 2, expected: outputs).Loss(predicted); // should be 1

        // We can also compute the adjusted or weighted versions of r² using
        var r2loss = new RSquaredLoss(numberOfInputs: 2, expected: outputs)
        {
            Adjust = true,
            // Weights = weights; // (if you have a weighted problem)
        };

        double ar2 = r2loss.Loss(predicted); // should be 1

        // Alternatively, we can also use the less generic, but maybe more user-friendly method directly:
        double ur2 = regression.CoefficientOfDetermination(inputs, outputs, adjust: true); // should be 1
    }

    public void PerformAprioriAnalysis()
    {// Example from https://en.wikipedia.org/wiki/Apriori_algorithm

        // Assume that a large supermarket tracks sales data by stock-keeping unit
        // (SKU) for each item: each item, such as "butter" or "bread", is identified 
        // by a numerical SKU. The supermarket has a database of transactions where each
        // transaction is a set of SKUs that were bought together.

        // Let the database of transactions consist of following itemsets:

        SortedSet<int>[] dataset =
        {
    // Each row represents a set of items that have been bought 
    // together. Each number is a SKU identifier for a product.
    new SortedSet<int> { 1, 2, 3, 4 }, // bought 4 items
    new SortedSet<int> { 1, 2, 4 },    // bought 3 items
    new SortedSet<int> { 1, 2 },       // bought 2 items
    new SortedSet<int> { 2, 3, 4 },    // ...
    new SortedSet<int> { 2, 3 },
    new SortedSet<int> { 3, 4 },
    new SortedSet<int> { 2, 4 },
};

        // We will use Apriori to determine the frequent item sets of this database.
        // To do this, we will say that an item set is frequent if it appears in at 
        // least 3 transactions of the database: the value 3 is the support threshold.

        // Create a new a-priori learning algorithm with support 3
        Apriori apriori = new Apriori(threshold: 3, confidence: 0);

        // Use the algorithm to learn a set matcher
        AssociationRuleMatcher<int> classifier = apriori.Learn(dataset);

        // Use the classifier to find orders that are similar to 
        // orders where clients have bought items 1 and 2 together:
        int[][] matches = classifier.Decide(new[] { 1, 2 });

        // The result should be:
        // 
        //   new int[][]
        //   {
        //       new int[] { 4 },
        //       new int[] { 3 }
        //   };

        // Meaning the most likely product to go alongside the products
        // being bought is item 4, and the second most likely is item 3.

        // We can also obtain the association rules from frequent itemsets:
        AssociationRule<int>[] rules = classifier.Rules;

        // The result will be:
        // {
        //     [1] -> [2]; support: 3, confidence: 1, 
        //     [2] -> [1]; support: 3, confidence: 0.5, 
        //     [2] -> [3]; support: 3, confidence: 0.5, 
        //     [3] -> [2]; support: 3, confidence: 0.75, 
        //     [2] -> [4]; support: 4, confidence: 0.66, 
        //     [4] -> [2]; support: 4, confidence: 0.8, 
        //     [3] -> [4]; support: 3, confidence: 0.75, 
        //     [4] -> [3]; support: 3, confidence: 0.6 
        // };
    }
}
