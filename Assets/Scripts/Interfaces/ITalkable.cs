using System.Collections;
using System.Collections.Generic;

public interface ITalkable
{
    string TalkerName{get; set;}
    string StartNode{get; set;}
    void talk();
}
