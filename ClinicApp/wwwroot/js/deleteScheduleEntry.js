document.addEventListener("click", async (event) => {
    const btn = event.target;

    if (btn.classList.contains("btn-schedule-delete")) {
        if (confirm("Are you sure you want to delete this entry?")) {
            const id = btn.getAttribute("schedule-id");
            await fetch(`/manage/schedule/deleteentry/${id}`, {
                method: "DELETE"
            });

            location.reload();
        }
    }
});