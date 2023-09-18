using System.Drawing;
using System.Reflection;
class BitmapStegoBuilder
{
    static void Main(string[] args)
    {
        SelectionMenu(args);
    }


    /**
     * Generates a bitmap image from a file containing raw byte data and saves it to the specified path.
     *
     * @param file The path to the input file containing raw byte data.
     * @param path The path where the generated bitmap image will be saved.
     * @return A Bitmap object representing the generated image.
     * @throws ArgumentException If the `file` or `path` is null or empty.
     * @throws InvalidOperationException If the dimensions of the file are invalid.
     * @throws UnauthorizedAccessException If there's an unauthorized access exception during file operations.
     * @throws IOException If an I/O exception occurs during file operations.
     * @remarks This function is 100% from ChatGPT, out of obligation.
     */

    #pragma warning disable CA1416
    static Bitmap GenerateBitmap(string file, string path)
    {

        if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("File and path arguments cannot be null or empty.");
        }

        try
        {
            file = Path.GetFullPath(file);

            byte[] fileBytes = File.ReadAllBytes(file);

            int fileDimensions = (int)Math.Sqrt(fileBytes.Length);
            if (fileDimensions <= 0)
            {
                throw new InvalidOperationException("File dimensions must be greater than zero.");
            }

            Bitmap bmp = new Bitmap(fileDimensions, fileDimensions);

            int idx = 0;

            for (int x = 0; x < fileDimensions; x++)
            {
                for (int y = 0; y < fileDimensions; y++)
                {
                    if (idx < fileBytes.Length)
                    {
                        byte redValue = fileBytes[idx];
                        Color newColor = Color.FromArgb(255, redValue, 0, 0);  // Alpha, Red, Green, Blue
                        bmp.SetPixel(x, y, newColor);
                        idx++;
                    }
                }
            }

            bmp.Save(path);

            Console.WriteLine("Bitmap successfully generated and saved.");

            return bmp;

        }
        catch (UnauthorizedAccessException e)
        {
            // Handle unauthorized access exception
            Console.WriteLine($"Unauthorized access: {e.Message}");
            throw;
        }
        catch (IOException e)
        {
            // Handle IO exceptions
            Console.WriteLine($"IO exception: {e.Message}");
            throw;
        }
        catch (ArgumentException e)
        {
            // Handle argument exceptions
            Console.WriteLine($"Argument exception: {e.Message}");
            throw;
        }
        catch (Exception e)
        {
            // Handle other exceptions
            Console.WriteLine($"An error occurred: {e.Message}");
            throw;
        }

    }

    /**
     * Converts a bitmap image to a text format, extracting red channel values.
     *
     * @param file The path to the input bitmap image file.
     * @param saveLocation The path where the converted text file will be saved.
     * @param psychedelic A flag indicating whether to enable psychedelic mode.
     * @return A list of bytes representing the red channel values of the image pixels.
     * @throws ArgumentException If the `file` or `saveLocation` is null or empty.
     * @throws FileNotFoundException If the specified `file` does not exist.
     * @throws IOException If an I/O error occurs during file operations.
     * @throws Exception If an unexpected error occurs.
     */
    private static List<byte> ConvertFromBitmap(string file, string saveLocation, bool psychedelic)
    {
        List<byte> redChannelValues = new List<byte>();
        List<string> redChannelHexValues = new List<string>();
        string joinedText = "";

        try
        {
            if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(saveLocation))
            {
                throw new ArgumentException("File path or save location cannot be null or empty.");
            }

            file = Path.GetFullPath(file);
            if (!File.Exists(file))
            {
                throw new FileNotFoundException($"File not found: {file}");
            }
            using (Bitmap bmp = new Bitmap(file))
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color pixelColor = bmp.GetPixel(x, y);
                        byte redValue = pixelColor.R;
                        string rawHex = redValue.ToString("X2");

                        redChannelValues.Add(redValue);
                        redChannelHexValues.Add(rawHex);
                    }
                }

                joinedText = string.Join(" ", redChannelHexValues.ToArray());
            }

            File.WriteAllText(saveLocation, joinedText);

            Console.WriteLine("Bitmap successfully converted to text format at location: " +
                saveLocation);

            if (psychedelic == true)
            {
                Console.WriteLine("You are in psychedelic mode, hit any key to continue.  Use CTRL+C to exit the program.");
                Console.ReadKey();

                PrintRainbowText(joinedText, 1);
            }

        }
        catch (ArgumentException ae)
        {
            Console.WriteLine($"Argument error: {ae.Message}");
        }
        catch (FileNotFoundException fnfe)
        {
            Console.WriteLine($"File not found: {fnfe.Message}");
        }
        catch (IOException ioe)
        {
            Console.WriteLine($"I/O error: {ioe.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }

        return redChannelValues;
    }


    /**
     * Prints text in a rainbow style, with changing colors.
     *
     * @param text The text to be printed in rainbow colors.
     * @param delayMilliseconds The delay in milliseconds between printing each character.
     * @remarks This function is 100% from ChatGPT, out of obligation.
     */
    public static void PrintRainbowText(string text, int delayMilliseconds)
    {
        ConsoleColor[] colors = {
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkYellow,  // Note that this is actually brown
            ConsoleColor.Gray,
            ConsoleColor.DarkGray,
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Red,
            ConsoleColor.Magenta,
            ConsoleColor.Yellow,
            ConsoleColor.White
        };

        Random random = new Random();  // Initialize the random number generator

        foreach (char c in text)
        {
            // Skip changing color for space characters
            if (c != ' ')
            {
                int randomIndex = random.Next(colors.Length);  // Generate a random index
                Console.ForegroundColor = colors[randomIndex];
            }
            Console.Write(c);

            // Introduce a delay between characters
            Thread.Sleep(delayMilliseconds);
        }

        Console.ResetColor();  // Reset to default color
        Console.WriteLine();   // Move to the next line
    }



static void SelectionMenu(string[] args)
    {

        string logo = @"

              ____  _ _                            _____ _                     ____        _ _     _           
             |  _ \(_) |                          / ____| |                   |  _ \      (_) |   | |          
             | |_) |_| |_ _ __ ___   __ _ _ __   | (___ | |_ ___  __ _  ___   | |_) |_   _ _| | __| | ___ _ __ 
             |  _ <| | __| '_ ` _ \ / _` | '_ \   \___ \| __/ _ \/ _` |/ _ \  |  _ <| | | | | |/ _` |/ _ \ '__|
             | |_) | | |_| | | | | | (_| | |_) |  ____) | ||  __/ (_| | (_) | | |_) | |_| | | | (_| |  __/ |   
             |____/|_|\__|_| |_| |_|\__,_| .__/  |_____/ \__\___|\__, |\___/  |____/ \__,_|_|_|\__,_|\___|_|   
                                         | |                      __/ |                                        
                                         |_|                     |___/   

            By: Neil Finneran (0xanc1llary)

            ";

        Console.WriteLine(logo);

        var options = new Dictionary<string, (Action action, bool isSelected)>
        {
            { "-h", (() => {}, false) },
            { "--help", (() => {}, false) },
            { "-v", (() => {}, false) },
            { "--version", (() => {}, false) },
            { "-f", (() => {}, false) },
            { "-g", (() => {}, false) },
            { "-c", (() => {}, false) },
            { "-o", (() => {}, false) },
            { "-p", (() => {}, false) },
            { "--psychedelic", (() => {}, false) }

        };

        foreach (var arg in args)
        {
            if (options.TryGetValue(arg, out var tuple))
            {
                tuple.action();
                options[arg] = (tuple.action, true);  // Set the flag to true
            }
        }

        // get command line arguments

        Dictionary<string, string> argValues = new Dictionary<string, string>();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            if (options.TryGetValue(arg, out var tuple))
            {
                tuple.action();
                options[arg] = (tuple.action, true);  // Set the flag to true

                // Capture the value after the flag
                if (i + 1 < args.Length && !options.ContainsKey(args[i + 1]))
                {
                    argValues[arg] = args[i + 1];
                }
            }
        }

        if (options["-h"].isSelected || options["--help"].isSelected)
        {

            string help = @"
                    -h, --help Show help menu.
                    -v, --version Show version of Bitmap Stego Builder.
                    -g Generate bitmap.
                    -c Convert bitmap to raw bytes in text form.
                    -f [filename] Selected file to be converted into a bitmap.
                    -o [filename.bmp] Set the outfile bitmap name.       
                    -p, --psychedelic Because what is a console application without rainbow colours?
                ";

            Console.WriteLine(help);

        }

        if (options["-v"].isSelected || options["--version"].isSelected)
        {
            Console.WriteLine("BitmapStegoBuilder Version: " + Assembly.GetExecutingAssembly().GetName().Version);

        }

        if (options["-g"].isSelected && options["-f"].isSelected && options["-o"].isSelected)
        {

            string inputFilePath = argValues["-f"];
            string outputFilePath = argValues["-o"];

            try
            {
                GenerateBitmap(inputFilePath, outputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        if (options["-c"].isSelected && options["-f"].isSelected && options["-o"].isSelected)
        {

            string inputFilePath = argValues["-f"];
            string outputFilePath = argValues["-o"];

            try
            {
                if(options["-p"].isSelected || options["--psychedelic"].isSelected)
                {
                    ConvertFromBitmap(inputFilePath, outputFilePath, true);
                } else
                {
                    ConvertFromBitmap(inputFilePath, outputFilePath, false);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }


        }

    }
}