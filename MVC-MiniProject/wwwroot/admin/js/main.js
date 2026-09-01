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









let deleteAuthorBtns = document.querySelectorAll(".delete-author");
deleteAuthorBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        e.preventDefault();
        let authorId = parseInt(this.getAttribute("data-id"));
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
                await deleteAuthor(authorId);
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
async function deleteAuthor(id) {
    const url = `/admin/author/delete?id=${id}`;
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








let deletePositionBtns = document.querySelectorAll(".delete-position");
deletePositionBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        e.preventDefault();
        let positionId = parseInt(this.getAttribute("data-id"));
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
                await deletePosition(positionId);
                this.parentNode.parentNode.remove()
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    });
});

async function deletePosition(id) {
    const url = `/admin/position/delete?id=${id}`;
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







let deleteNewsBtns = document.querySelectorAll(".delete-news");
deleteNewsBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        e.preventDefault();
        let newsId = parseInt(this.getAttribute("data-id"));
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
                await deleteNews(newsId);
                this.parentNode.parentNode.remove()
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    });
});

async function deleteNews(id) {
    const url = `/admin/news/delete?id=${id}`;
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






let deleteIconBtns = document.querySelectorAll(".delete-icon");
deleteIconBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        e.preventDefault();
        let iconId = parseInt(this.getAttribute("data-id"));
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
                await deleteIcon(iconId);
                this.parentNode.parentNode.remove()
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
            }
        });
    });
});

async function deleteIcon(id) {
    const url = `/admin/icon/delete?id=${id}`;
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





let deleteSliderBtns = document.querySelectorAll(".delete-slider");
deleteSliderBtns.forEach(btn => {
    btn.addEventListener("click", async function (e) {
        console.log("KLIK OLDU, id:", this.getAttribute("data-id")); // TEST ÜÇÜN ƏLAVƏ OLUNDU
        e.preventDefault();
        let sliderId = parseInt(this.getAttribute("data-id"));
        console.log("sliderId parse edildi:", sliderId); // TEST ÜÇÜN ƏLAVƏ OLUNDU
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
                await deleteSlider(sliderId);
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
async function deleteSlider(id) {
    const url = `/admin/slider/delete?id=${id}`;
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