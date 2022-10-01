<template src="./Tests.html"></template>
<script>
import TestList from "@/components/TestList";
import UserTests from "@/components/UserTests";
import { mapActions, mapGetters } from "vuex";
export default {
  name: "Tests",
  data() {
    return {
      navItem: 1,
    };
  },
  methods: {
    ...mapActions(["loadTests", "getQuestions", "delTest"]),
    deleteTest(id) {
      this.delTest(id);
    },
  },
  computed: {
    ...mapGetters(["Tests", "UserTests"]),
  },
  async created() {
    await this.loadTests();
  },
  components: {
    TestList,
    UserTests,
  },
};
</script>
<style scoped src="./Tests.css"></style>
