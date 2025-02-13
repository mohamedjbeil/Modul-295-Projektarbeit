document.addEventListener("DOMContentLoaded", function () {
  const form = document.getElementById("serviceForm");
  const serviceSelect = document.getElementById("service");
  const totalPriceInput = document.getElementById("totalPrice");
  const pickupDateInput = document.getElementById("pickupDate");

  if (!form) {
      console.error("Formular nicht gefunden!");
      return;
  }

  form.addEventListener("input", function () {
      const selectedService = serviceSelect.options[serviceSelect.selectedIndex];
      const basePrice = parseFloat(selectedService?.dataset.price || 0);

      totalPriceInput.value = basePrice > 0 ? `${basePrice} CHF` : "";

      const daysToAdd = 7; // Standardzeit
      const currentDate = new Date();
      const pickupDate = new Date(currentDate.setDate(currentDate.getDate() + daysToAdd));
      pickupDateInput.value = pickupDate.toLocaleDateString();
  });

  form.addEventListener("submit", async function (e) {
      e.preventDefault();

      if (!form.checkValidity()) {
          form.classList.add("was-validated");
          return;
      }

      const formData = {
          name: document.getElementById("name").value.trim(),
          email: document.getElementById("email").value.trim(),
          phone: document.getElementById("phone").value.trim(),
          service: serviceSelect.value,
          totalPrice: totalPriceInput.value,
          pickupDate: pickupDateInput.value,
      };

      try {
        const response = await fetch("http://localhost:5000/api/registrations", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(formData),
      });      

          if (response.ok) {
              alert("Anmeldung erfolgreich!");
              form.reset();
          } else {
              alert("Fehler bei der Anmeldung.");
          }
      } catch (error) {
          alert("Netzwerkfehler.");
      }
  });
});
