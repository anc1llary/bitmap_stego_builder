# bitmap_stego_builder

This is a small command line utility that leverages a common stegonography technique by convertin raw bytes into pixels in a bitmap.

## Command line options

- -h, --help Show help menu.
- -v, --version Show version of Bitmap Stego Builder.
- -g Generate bitmap.
- -c Convert bitmap to raw bytes in text form.
- -f [filename] Selected file to be converted into a bitmap.
- -o [filename.bmp] Set the outfile bitmap name.       
- -p, --psychedelic Because what is a console application without rainbow colours?

## Changelog

- Add functionality to select various channels available, right now only red channel is supported.
- Create functionality for junk bytes to be thrown into un-used channels for further obfsucation.
