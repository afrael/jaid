#!/bin/bash

# Variables
slndir="$(dirname "${BASH_SOURCE[0]}")"

# Build
dotnet build "$slndir/jaid.sln" --nologo || exit

# Test
# dotnet test "$slndir/src/gitdoc.tests" --nologo --no-build
