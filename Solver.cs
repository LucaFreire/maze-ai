using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Stately;

public class Solver
{
    public Maze Maze { get; set; }
    public void Solve()
    {
        var Root = Maze.Root;
        // RecursiveDFS(Root);
        // DFS(Root);
        // BFS(Root);
    }

    public bool RecursiveDFS(Space space)
    {
        var child = space.GetChildrenStack();

        while (child.Count > 0)
        {
            var crr = child.Pop();
            crr.Visited = true;

            if (crr.Exit)
            {
                crr.IsSolution = true;
                space.IsSolution = true;
                return true;
            }

            space.IsSolution = RecursiveDFS(crr);
            if (space.IsSolution)
                return true;
        }
        return false;
    }
    public void DFS(Space root) // Fix to show the best path
    {
        var stack = new Stack<Space>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var crr = stack.Pop();
            crr.Visited = true;

            if (crr.Exit)
            {
                foreach (var item in stack)
                    item.IsSolution = true;
                return;
            }

            if (crr.Left is not null && !crr.Left.Visited)
                stack.Push(crr.Left);

            if (crr.Right is not null && !crr.Right.Visited)
                stack.Push(crr.Right);

            if (crr.Top is not null && !crr.Top.Visited)
                stack.Push(crr.Top);

            if (crr.Bottom is not null && !crr.Bottom.Visited)
                stack.Push(crr.Bottom);
        }
    }
    public void BFS(Space root) // Fix to show the best path
    {
        var queue = new Queue<Space>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            var crr = queue.Dequeue();
            crr.Visited = true;

            if (crr.Exit)
            {
                foreach (var item in queue)
                    item.IsSolution = true;
                return;
            }

            if (crr.Left is not null && !crr.Left.Visited)
                queue.Enqueue(crr.Left);

            if (crr.Right is not null && !crr.Right.Visited)
                queue.Enqueue(crr.Right);

            if (crr.Top is not null && !crr.Top.Visited)
                queue.Enqueue(crr.Top);

            if (crr.Bottom is not null && !crr.Bottom.Visited)
                queue.Enqueue(crr.Bottom);
        }
    }
}

public static class SolverExtensions
{
    public static Stack<Space> GetChildrenStack(this Space space)
    {
        var stack = new Stack<Space>();

        if (space.Left is not null && !space.Left.Visited)
            stack.Push(space.Left);

        if (space.Right is not null && !space.Right.Visited)
            stack.Push(space.Right);

        if (space.Top is not null && !space.Top.Visited)
            stack.Push(space.Top);

        if (space.Bottom is not null && !space.Bottom.Visited)
            stack.Push(space.Bottom);

        return stack;
    }
    public static Stack<Space> Murilove(this Space root)
    {

        Stack<Space> children = new();
        children.Push(root);

        while (children.Count > 0)
        {
            var current = children.Peek();
            current.Visited = true;

            if (current.Exit)
                break;

            if (current.Right is not null && !current.Right.Visited)
                children.Push(current.Right);
            
             if (current.Bottom is not null && !current.Bottom.Visited)
                children.Push(current.Bottom);
            
             if (current.Top is not null && !current.Top.Visited)
                children.Push(current.Top);
            
             if (current.Left is not null && !current.Left.Visited)
                children.Push(current.Left);

            if (current == children.Peek())
                children.Pop();
        }

        foreach (var son in children)
            son.IsSolution = true;
        
        return children;
    }
}