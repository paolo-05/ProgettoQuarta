﻿NDSummary.OnToolTipsLoaded("File:PlayerController.cs",{78:"<div class=\"NDToolTip TClass LCSharp\"><div class=\"NDClassPrototype\" id=\"NDClassPrototype78\"><div class=\"CPEntry TClass Current\"><div class=\"CPModifiers\"><span class=\"SHKeyword\">public</span></div><div class=\"CPName\">PlayerController</div></div></div><div class=\"TTSummary\">Controls the behavior of the player character in the game.</div></div>",80:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype80\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> forwardSpeed</div></div><div class=\"TTSummary\">The forward speed of the player.</div></div>",81:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype81\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> maxSpeed</div></div><div class=\"TTSummary\">The maximum speed of the player.</div></div>",82:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype82\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> laneDistance</div></div><div class=\"TTSummary\">The distance between two lanes.</div></div>",83:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype83\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public bool</span> isGrounded</div></div><div class=\"TTSummary\">Determines if the player is grounded.</div></div>",84:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype84\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public</span> LayerMask groundLayer</div></div><div class=\"TTSummary\">The layer mask for the ground.</div></div>",85:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype85\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public</span> Transform groundCheck</div></div><div class=\"TTSummary\">The transform used for checking if the player is grounded.</div></div>",86:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype86\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public</span> Animator animator</div></div><div class=\"TTSummary\">The animator component for controlling animations.</div></div>",87:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype87\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> gravity</div></div><div class=\"TTSummary\">The gravity value affecting the player.</div></div>",88:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype88\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> jumpHeight</div></div><div class=\"TTSummary\">The height of the player\'s jump.</div></div>",89:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype89\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> slideDuration</div></div><div class=\"TTSummary\">The duration of the sliding animation.</div></div>",90:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype90\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public float</span> speedIncreasePerPoint</div></div><div class=\"TTSummary\">The speed increase per point scored.</div></div>",91:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype91\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private</span> CharacterController controller</div></div></div>",92:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype92\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private</span> Vector3 move</div></div></div>",93:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype93\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private int</span> desiredLane</div></div></div>",94:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype94\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private</span> Vector3 velocity</div></div></div>",95:"<div class=\"NDToolTip TVariable LCSharp\"><div id=\"NDPrototype95\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private bool</span> isSliding</div></div></div>",97:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype97\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">void</span> Start()</div></div></div>",98:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype98\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">void</span> Update()</div></div></div>",99:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype99\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private void</span> Jump()</div></div><div class=\"TTSummary\">Makes the player character jump.</div></div>",100:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype100\" class=\"NDPrototype WideForm\"><div class=\"PSection PParameterSection CStyle\"><table><tr><td class=\"PBeforeParameters\"><span class=\"SHKeyword\">private void</span> OnControllerColliderHit(</td><td class=\"PParametersParentCell\"><table class=\"PParameters\"><tr><td class=\"PType first\">ControllerColliderHit&nbsp;</td><td class=\"PName last\">hit</td></tr></table></td><td class=\"PAfterParameters\">)</td></tr></table></div></div></div>",101:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype101\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">private</span> IEnumerator Slide()</div></div></div>",102:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype102\" class=\"NDPrototype WideForm\"><div class=\"PSection PParameterSection CStyle\"><table><tr><td class=\"PBeforeParameters\"><span class=\"SHKeyword\">private</span> IEnumerator EnemyCollision(</td><td class=\"PParametersParentCell\"><table class=\"PParameters\"><tr><td class=\"PType first\">ControllerColliderHit&nbsp;</td><td class=\"PName last\">hit</td></tr></table></td><td class=\"PAfterParameters\">)</td></tr></table></div></div></div>",103:"<div class=\"NDToolTip TFunction LCSharp\"><div id=\"NDPrototype103\" class=\"NDPrototype\"><div class=\"PSection PPlainSection\"><span class=\"SHKeyword\">public void</span> Die()</div></div><div class=\"TTSummary\">Ends the game when the player dies.</div></div>"});