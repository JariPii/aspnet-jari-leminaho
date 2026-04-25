// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

console.log('site.js loaded');

document.addEventListener('DOMContentLoaded', function () {
  const btn = document.getElementById('mobile-menu-btn');
  const menu = document.getElementById('mobile-menu');

  console.log(btn, menu);

  if (btn && menu) {
    btn.addEventListener('click', function () {
      console.log('clicked');
      menu.classList.toggle('hidden');
    });
  }
});
