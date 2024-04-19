import $ from "jquery";

console.log("Module-2.ts");

$(() => {
	$("main").append("<h2>jQuery</h2><div class=\"alert alert-info\"><h3>Appended</h3><p>Appended in Module-2.ts.</p></div>");
});