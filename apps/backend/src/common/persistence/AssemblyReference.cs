﻿using System.Reflection;

namespace Thread.Persistence;

/// <summary>
/// Represents the persistence assembly reference.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// The assembly.
    /// </summary>
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

