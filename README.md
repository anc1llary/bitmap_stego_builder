# bitmap_stego_builder

This is a compact command line tool that employs a widely used steganography method, which entails encoding raw bytes as pixels within a bitmap image.  Threat actors leverage this technique to store staged payloads for post execution.

## Command line options

- -h, --help Show help menu.
- -v, --version Show version of Bitmap Stego Builder.
- -g Generate bitmap.
- -c Convert bitmap to raw bytes in text form.
- -f [filename] Selected file to be converted into a bitmap.
- -o [filename.bmp] Set the outfile bitmap name.       
- -p, --psychedelic Because what is a console application without rainbow colours?

## Media

![ChatGPT OSINT Plugin Demo](https://github.com/anc1llary/bitmap_stego_builder/demo.png)


## Changelog

- Add functionality to select various channels available, right now only red channel is supported.
- Create functionality for junk bytes to be thrown into un-used channels for further obfsucation.

### Credits and Resources

- MSDN (https://learn.microsoft.com/en-us/dotnet/api/system.drawing.bitmap?view=dotnet-plat-ext-7.0)
- Generative AI for modification to some functions and comments.
- ASCII Logo Generator (https://patorjk.com/software/taag/#p=display&f=Graffiti&t=Type%20Something%20)