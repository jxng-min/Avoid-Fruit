mergeInto(LibraryManager.library, {
    SendData: function(json_data) {
        const token = localStorage.getItem("accessToken");
        console.log("Retrieved token:", token);

        let userId = 2;

        if (!token) {
            console.error("토큰이 없습니다. 로그인 후 다시 시도하세요.");
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
            userId = data.payload;
            console.log("받은 유저 ID:", userId);
        })
        .catch(error => {
            console.error("유저 ID 요청 중 오류 발생:", error);
        });

        if (typeof json_data !== "string") {
            json_data = String(json_data);  // 숫자형을 문자열로 변환
        }
        
        console.log("Received JSON string:", json_data);
        const data = JSON.parse(json_data);
        console.log("Data to send:", data);

        const jsonData = JSON.stringify({
            userId: userId,
            gameCategory: "Fruit",
            score: data.score
        });
        console.log("JSON Data to send:", jsonData);

        fetch("http://3.34.98.225:8080/api/v1/game/score", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: jsonData
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
