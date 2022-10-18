<template src="./ForgotPassword.html"> </template>
<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data() {
    return {
      IsSent: false,
      email: "",
    };
  },
  methods: {
    ...mapActions(["sendResetLink", "resetErrors"]),
    async Send() {
      await this.sendResetLink(this.email);
      if (this.ResetLinkResponse.success) {
        this.IsSent = true;
        this.Email = "";
        this.resetErrors();
      }
    },
  },
  computed: {
    ...mapGetters(["errorResponse", "ResetLinkResponse"]),
  },
};
</script>
<style scoped src="./ForgotPassword.css"></style>
