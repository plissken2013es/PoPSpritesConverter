﻿#region MIT License

/*
 * Copyright (c) 2016 Marcelo Lv Cabral (http://github.com/lvcabral)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
 * 
 */

#endregion

using System;
using System.IO;

namespace popsc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialization
            Console.WriteLine("");
            Console.WriteLine("PoP 1 Sprites Converter v1.0 - (C)2016 Marcelo Lv Cabral");
            Console.WriteLine("Convert bmp extracted from PoP 1 dat files into png frames and sheets");
            Console.WriteLine("<< Focused on converting Mods to Prince of Persia for Roku >>");
            Console.WriteLine("<< Source code is avaliable at: http://github.com/lvcabral >>");
            if (args.Length >= 2)
            {
                if (!Directory.Exists(args[0]))
                {
                    Console.WriteLine("PR resources folder does not exist!");
                    return;
                }
                if (!Directory.Exists(args[1]))
                {
                    Console.WriteLine("Output folder does not exist!");
                    return;
                }
                bool wda = false;
                if (args.Length > 2)
                {
                    if (args[2].ToLower().Trim() == "-wda")
                    {
                        wda = true;
                    }
                    else
                    {
                        help();
                        return;
                    }
                }
                // Convert Sprites
                bool ok = Tiles.convertTiles("dungeon", args[0], args[1]);
                if (ok) ok = Tiles.convertTiles("palace", args[0], args[1], wda);
                if (ok && File.Exists(Path.Combine(args[0], @"prince\binary\level color variations.pal")))
                {
                    if (ok) ok = Tiles.convertTiles("dungeon", args[0], args[1], wda, 0);
                    if (ok) ok = Tiles.convertTiles("dungeon", args[0], args[1], wda, 1);
                    if (ok) ok = Tiles.convertTiles("dungeon", args[0], args[1], wda, 2);
                    if (ok) ok = Tiles.convertTiles("palace", args[0], args[1], wda, 3);
                }
                if (ok) ok = Kid.convertKid(args[0], args[1]);
                if (ok) ok = Guards.convertGuards(args[0], args[1]);
                if (ok) ok = Guards.convertSpecialGuards(args[0], args[1]);
                if (ok) ok = Actors.convertActors(args[0], args[1]);
                if (ok) ok = General.convertGeneral(args[0], args[1]);
                if (ok) ok = Scenes.convertScenes(args[0], args[1]);
                if (ok) ok = Titles.convertTitles(args[0], args[1]);
                if (!ok) Console.ReadKey();
            }
            else
            {
                help();
            }
        }

        private static void help()
        {
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("popsc <PR resources path> <sprites output path> [-wda]");
            Console.WriteLine("Optional parameter:");
            Console.WriteLine("-wda : Convert VPALACE wall images using wall-drawing algorithm mode");
        }
    }
}
