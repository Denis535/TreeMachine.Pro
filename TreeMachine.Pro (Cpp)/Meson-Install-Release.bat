# meson compile -C "Build/Release" --clean
# meson compile -C "Build/Release"
meson install -C "Build/Release" --destdir="Distribution"
pause
