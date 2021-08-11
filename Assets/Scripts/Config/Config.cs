using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimator
{
    public static class Parameters
    {
        public const string Horizontal = nameof(Horizontal);
        public const string Vertical = nameof(Vertical);
        public const string Jump = nameof(Jump);
        public const string Grounded = nameof(Grounded);
        public const string Fall = nameof(Fall);
    }
}

public static class ZombieAnimator
{
    public static class Parameters
    {
        public const string Punch = nameof(Punch);
    }
    
    public static class Animations
    {
        public const string Walk = nameof(Walk);
    }
}