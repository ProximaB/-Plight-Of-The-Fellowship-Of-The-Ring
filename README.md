# -Plight-Of-The-Fellowship-Of-The-Ring
 Plight Of The Fellowship Of The Ring (codingame.com puzzle)
Story:

Help the fellowship! They are trapped in the mines of Moria, with orcs closing in from all sides. Their only hope is Gandalf’s wizardry. Gandalf can create portals through which they can quickly travel from one spot to another. However portals can be opened in limited spots only, and in a limited number of ways. Your program must print the path they should follow to be safely out.

--------------------------------------------------- xxx ---------------------------------------------------

Rules:

The Spots will be INDEXED as 0, 1, 2, 3, ...

You will be given the COORDINATES OF THE SPOTS to where Gandalf can create a portal.

You will be given the COORDINATES OF THE ORCS.

You will also be given the POSSIBLE PATHS from one spot to another. (the indexes)
(note: the paths are double sided. this means that if a path is possible from spot 2 to spot 5, then it is possible to go from 2 to 5, as also from 5 to 2)

--------------------------------------------------- xxx ---------------------------------------------------

The Problem:

Your algorithm should display the sequence of the spots in order of how the fellowship go in order to reach the end fastest, and safely.
(note: the fellowship can travel from one spot to another along the POSSIBLE PATHS only)

You will be given the index of the STARTING SPOT and also the ENDING SPOT.

--------------------------------------------------- xxx ---------------------------------------------------

Note:

1. For every move of the fellowship, each orc can move by a distance of 1. If you need N moves to reach a spot, and distance from the starting point of an orc to that spot is ≤ N, you cannot go there or you might be killed.

For example, if spot 2 and spot 5 are connected by a path, and coordinates of 2 is (1,0) and 5 is (2,2), and an orc is present at (1,2), then the fellowship cannot go from spot 2 to spot 5, as time taken by the fellowship is 1 UNIT, and distance moved by the orc in 1 unit time is 1 UNIT ((2,1) and (2,2) are separated by distance 1 UNIT).

2. The fellowship can only move along the paths from one spot to another.

For example, if spot 2 and spot 5 are connected by a path, and coordinates of 2 is (1,0) and 5 is (2,2), then from 2 the fellowship can move only to 5, and not to any random spot {say (1,2)} [i.e. they can only move to a spot, which is connected] .

3. Distances are calculated by the distance formula: dist = sqrt( (p1.x-p2.x)^2 + (p1.y-p2.y)^2 ) (i.e. distances are Pythagorean)

--------------------------------------------------- xxx ---------------------------------------------------
