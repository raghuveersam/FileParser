# FileParser

FileParser is a C# winform application that computes the list of words that can be generated with other string in the given input file

## Usage

Load the input file to the project. In the sample, the file is named as "InputData.txt" and then execute the application. You should be displayed with the Results in a form. 

## Problem Description 
Write a program that reads a file containing a sorted list of words, then identifies:
1. The longest word in the file that can be constructed by concatenating copies of shorter words also found in the file. 
2. The program should then go on to report second largest word found.
3. Total count of how many of the words in the list can be constructed of other words in the list. 
4. Make sure the solution is optimized to handle large data.

## Implementation

1. Read the file and determine the minimum length and second minimum length and also store these words in a dictionary( This will help for easy lookup)
2. Loop through the word list and ONLY compute words that has length more than the sum of minimum length and second minimum length as we are only interested on the concatenated words. 
3. If the current word can be computed with the other words in the file. Perform the following:
          i. Add these words for a separate word list as this word can be used for upcoming words(e.g: abc, efg, abcefg, ij, abcefgij) => This can be computer with abcefg + ij rather than abc+efg+ij
          ii. Determine the largest and second largest word in the result dictionary.

