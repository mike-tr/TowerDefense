using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {
    public static List<Node> FindPath (Node start, Node end) {
        List<Node> open = new List<Node> ();
        HashSet<Node> close = new HashSet<Node> ();
        start.ResetCost ();

        open.Add (start);
        int c = 0;
        int maxRuns = start.board.sizeX * start.board.sizeY;
        while (open.Count > 0 && c < maxRuns) {
            var current = open[0];
            if (current == end) {
                return TraversePath (end, start);
            }

            open.Remove (current);
            close.Add (current);

            foreach (var node in current.GetNeighbours ()) {
                if (node.walkable && !close.Contains (node)) {
                    if (!open.Contains (node)) {
                        node.ResetCost ();
                    }
                    if (node.UpdateCost (start, current)) {
                        InsertNode (open, node);
                    }
                }
            }
            c++;
        }
        return null;
    }

    private static void InsertNode (List<Node> open, Node node) {
        if (open.Contains (node)) {
            open.Remove (node);
        }
        int i = 0;
        for (; i < open.Count; i++) {
            if (node.cost < open[i].cost) {
                break;
            }
        }

        open.Insert (i, node);
    }

    private static List<Node> TraversePath (Node end, Node start) {
        List<Node> path = new List<Node> ();
        var current = end;
        path.Add (current);
        while (current.pivot != null && current != start) {
            current = current.pivot;
            path.Add (current);
        }
        return path;
    }

    public int xstart, ystart;
    public int xend, yend;
    public Board board;
    public bool recalculate = true;
    private List<Node> path = new List<Node> ();

    private void Update () {
        if (recalculate && board.map != null) {
            if (path != null)
                foreach (var node in path) {
                    node.ResetColor ();
                }

            var start = board.GetNode (xstart, ystart);
            var end = board.GetNode (xend, yend);
            if (start == null || end == null) {
                return;
            }
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
            sw.Start ();
            path = FindPath (start, end);
            sw.Stop ();
            // Debug.Log (sw.ElapsedMilliseconds);
            if (path != null) {
                foreach (var node in path) {
                    node.ColorNode (Color.white);
                }
            }
        }
    }
}
