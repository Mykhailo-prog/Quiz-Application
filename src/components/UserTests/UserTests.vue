<template src="./UserTests.html"></template>
<script>
import QuestInput from "@/components/QuestInput";
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      QuestCounter: 1,
      AddedQuestCounter: 0,
      NewTest: { name: null },
      NewQuests: [],
    };
  },
  methods: {
    ...mapActions(["postNewTest", "loadTests"]),
    async CreateTest() {
      await this.postNewTest({
        test: this.NewTest,
        quests: this.NewQuests,
        user: this.CurrUser,
      });
      await this.loadTests();
      this.$bvModal.hide("modal-scoped");
    },
    onSubmit(event) {
      event.preventDefault();
    },
    checkQuest(quests, id) {
      this.NewQuests[id] = quests;
    },
    delQuest() {
      this.QuestCounter--;
      this.NewQuests.pop();
    },
  },
  computed: {
    ...mapGetters(["currUser"]),
    CurrUser() {
      return this.currUser;
    },
  },
  components: {
    QuestInput,
  },
};
</script>
<style scoped src="./UserTests.css"></style>
