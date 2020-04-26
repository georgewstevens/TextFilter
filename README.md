# TextFilter

## Usage guide
Run the executable with 2 command line arguments as follows:

`TextFilter.exe <filter> ""<filepath>""`


`<filter> is the filter you wish to use. It should be one of [MiddleVowel|LessThan3|ContainsT|All3].`

`<filepath> is a filepath to the text to filter. If it contains spaces, please wrap in quotes as shown above.`

In order to see this usage printed by the application, run it with less than 2 arguments.

i.e. `TextFilter.exe -h`

In order to run all 3 filters on the alice in wonderland sample text, move the Alice.txt file into the same directory as the executable and run the all 3 filter.

i.e. `TextFilter.exe All3 ""Alice.txt""`

Note that visual studio has been setup with these params for running in debug, and with the Alice.txt file copied to the binary executable's output directory.

## Notes

I have completed this task using .NET Core 3.1

I have interpretted words to be just letters (a-z). This means that any errant punctuation will be preserved by the filters.

Also, hyphenated and abbreviated words ("can-do", "Don't") will be counted as two separate words.


I was unsure whether the filters should all be run on the same original input string, or be run consecutively one after another.

I have therefore allowed the user to configure the project to run the filters individually, or all 3 at once.

This is configurable by selecting a filter using command line arguments.
