<template src="./Tests.html"></template>
<script>
import TestList from "@/components/TestList";
import { mapState, mapActions } from "vuex";
export default {
  name: "Tests",
  methods: {
    ...mapActions(["loadTests", "getQuestions"]),
    getQuest() {
      this.GetQuestions;
    },
  },
  computed: {
    ...mapState({
      tests: (state) => state.tests.testList,
      quests: (state) => state.tests.currTest.questions,
    }),
    GetQuestions() {
      this.getQuestions(this.quests);
      return true;
    },
    GetTests() {
      return this.tests;
    },
  },
  mounted() {
    this.loadTests();
    this.GetTests;
  },
  components: {
    TestList,
  },
};
</script>
<style scoped src="./Tests.css"></style>
