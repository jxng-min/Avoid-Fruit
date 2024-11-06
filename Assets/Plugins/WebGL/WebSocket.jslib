mergeInto(LibraryManager.library, {
    RequestUserId: function() {
        const token = localStorage.getItem("accessToken");
        console.log("Retrieved token:", token);

        if (!token) {
            console.error("토큰이 없습니다. 로그인 후 다시 시도하세요.");
            return;
        }

        fetch("http://3.34.98.225:8080/api/v1/game/userId", {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error("유저 ID 요청에 실패했습니다.");
            }
            return response.json();
        })
        .then(data => {
            const userId = data.payload;
            console.log("받은 유저 ID:", userId);

            unityInstance.SendMessage("WebClient11", "OnUserIdReceived", userId.toString());
        })
        .catch(error => {
            console.error("유저 ID 요청 중 오류 발생:", error);
        });
    },

    SendData: function(json_data) {
        var data = UTF8ToString(json_data);
        console.log("Data to send:", data);

        fetch("http://3.34.98.225:8080/api/v1/game/score", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: data
        })
        .then(response => response.json())
        .then(responseData => {
            console.log("Received response from server:", responseData);
        })
        .catch(error => {
            console.error("Error sending data:", error);
        });
    }
});
