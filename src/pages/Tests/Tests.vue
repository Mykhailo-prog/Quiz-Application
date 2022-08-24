<template src="./Tests.html"></template>
<script>
import TestList from "@/components/TestList";
import UserTests from "@/components/UserTests";
import { mapState, mapActions } from "vuex";
export default {
  name: "Tests",
  data() {
    return {
      navItem: 1,
    };
  },
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
    //Можна ініціалізувати через currTest!
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
    UserTests,
  },
};
</script>
<style scoped src="./Tests.css"></style>
