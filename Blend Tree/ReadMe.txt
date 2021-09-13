Download needed animations which are listed in the image in ZIP file from mixamo.

1) Pre-settings

-Go to animator tab and right click empty space and create blend tree.

-Go to parameters tab and create two float parameters with named 
"Velocity X" and "Velocity Z".

-Double-click blend tree and set according to image.

2) Scripting

-Call animator from library which named "anim" and
set your character's animator in Start().
(Example : blendTreeLibrary.anim = GetComponent<Animator>();)

-Call AnimatorController() method from library in Update().

You can use it anymore.

