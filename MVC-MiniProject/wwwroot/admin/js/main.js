"use strict";

let deleteEventBtns = document.querySelectorAll(".delete-event");
deleteEventBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        e.preventDefault();
        let eventId = parseInt(this.getAttribute("data-id"));
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then(async (result) => {
            if (result.isConfirmed) {
                await deleteEvent(eventId);
                this.parentNode.parentNode.remove()
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    });
})
async function deleteEvent(id) {
    const url = `/admin/event/delete?id=${id}`;
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const result = await response.json();
        return true;

    } catch (error) {
        console.error('Error during fetch:', error);
        return false;
    }
}