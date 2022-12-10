# Kamen

Simple tool for generating masked textures from a Texture2D Images.

**(GIF not available yet, sorry!)**

### Why

Sometimes you try an Asset to create FX effects for 2D games, and you eventually find that its custom Shader or Component by itself made the whole project even more difficult to manage. Sometimes a simpler, slower way is better for prototyping.

So, here I am, trying to make some pretty ghost trail-shadow and hit frame effects for my 2D sprites. You can't reuse the same sprite, because the Color property in the SpriteRenderer will TINT by Multiply, instead of replacing the texture colors...

Then I come with an stupid idea: What IF I used just a separated sprite? *I could use it on a BlendTree or insert it inside an AnimClip...*

This tool follows the KISS principle. Consider it as a 'shortcut' alternative for Photoshop for that purpose.


### What is this tool

It takes your texture, then previews and creates a copy of the original, but will all non-transparent colors painted as white.
This way, you can still tint your material with whatever color you want.


### What is not

It's intended to be used on 2D and/or pixelart games, mostly on prototypes. 
That being said, SpriteSheets are instantly a plus, as it reduces the amount of assets needed.

Kamen won't make you a Ghost Trail nor a Shadow Effect, that is on you.


### Author

Just me.



### Current Features

- Wizard with the tool, single file.
- Auto-detects if the texture has alpha channel, uses alpha instead.
- Allows to specify which color should be treated as transparent.
- Creates a copy of the original image, with the same import settings.
