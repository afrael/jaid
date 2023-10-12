#!/bin/bash

# Variables
slndir="$(dirname "${BASH_SOURCE[0]}")/src"

# Restore + Build
dotnet build "$slndir/jaid-cli" --nologo --verbosity quiet || exit

# Run
dotnet run --project "$slndir/jaid-cli" --no-build -- "$@"
