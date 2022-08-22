<template src="./LoginUser.html"></template>
<script>
import { mapActions } from "vuex";
export default {
  data() {
    return {
      userLogin: "",
    };
  },
  methods: {
    ...mapActions(["checkUser"]),
    checkUserLogin() {
      if (this.userLogin != "") {
        this.checkUser(this.userLogin);
        this.$router.push("/tests");
      }
    },
  },
};
</script>
<style scoped src="./LoginUser.css"></style>
