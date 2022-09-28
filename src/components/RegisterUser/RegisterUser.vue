<template src="./RegisterUser.html"> </template>
<script>
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      registerForm: {
        emailAddress: "",
        login: "",
        password: "",
        confirmPassword: "",
      },
    };
  },
  methods: {
    ...mapActions(["RegisterUser"]),
    async Register() {
      await this.RegisterUser(this.registerForm);
      if (this.RegisterResponse.success) {
        this.registerForm = {
          emailAddress: "",
          login: "",
          password: "",
          confirmPassword: "",
        };
        console.log("done");
        this.$router.push("/confirmEmail");
      }
    },
  },
  computed: {
    ...mapGetters(["RegisterResponse"]),
  },
};
</script>
<style scoped src="./RegisterUser.css"></style>
