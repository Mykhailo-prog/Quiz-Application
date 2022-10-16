<template src="./Tests.html"></template>
<script>
import TestList from "@/components/TestList";
import UserTests from "@/components/UserTests";
import AdminPage from "@/components/AdminPage";
import { mapActions, mapGetters } from "vuex";
export default {
  name: "Tests",
  data() {
    return {
      navItem: 1,
      isAdmin: false,
    };
  },
  methods: {
    ...mapActions(["loadTests", "getQuestions", "delTest", "checkRole"]),
    deleteTest(id) {
      this.delTest(id);
    },
  },
  computed: {
    ...mapGetters(["Tests", "UserTests"]),
  },
  async created() {
    await this.loadTests();
    this.isAdmin = await this.checkRole();
  },
  components: {
    TestList,
    UserTests,
    AdminPage,
  },
};
</script>
<style scoped src="./Tests.css"></style>
