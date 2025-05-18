document.getElementById('registerForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const name = document.getElementById('name').value.trim();
    const email = document.getElementById('email').value.trim();
    const password = document.getElementById('password').value;

    const user = {
        name: name,
        email: email,
        password: password
    };

    try {
        const response = await fetch('api/users/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });

        const resultContainer = document.getElementById('registerResult');

        if (response.ok) {
            const data = await response.json();
            resultContainer.textContent = `Користувача зареєстровано: ${data.name}`;
            resultContainer.style.color = 'green';
        } else {
            const errorText = await response.text();
            resultContainer.textContent = `Помилка: ${errorText}`;
            resultContainer.style.color = 'red';
        }
    } catch (error) {
        document.getElementById('registerResult').textContent = 'Сталася помилка: ' + error.message;
    }
});