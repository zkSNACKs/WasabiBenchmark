# shell.nix

with import <nixos> {};
let
  libs = [
    xorg.libX11
    xorg.libX11.dev
    xorg.libICE
    xorg.libSM
    fontconfig.lib
  ];

in
mkShell {
  name = "dotnet-env";
  buildInputs = libs;

  LD_LIBRARY_PATH = "${lib.makeLibraryPath libs}";
}
