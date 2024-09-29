<template>
  <Suspense>
    <v-container fluid>
      <v-row>
        <v-col>
          <v-btn prepend-icon="$plus" @click="addFund()"> Добавить </v-btn>
        </v-col>
      </v-row>

      <v-row v-if="editingValue">
        <v-col>
          <v-card>
            <v-card-title>
              <span class="text-h5">Редактирование</span>
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-text-field v-model="editingValue.name" label="Название"></v-text-field>
                </v-col>
                <v-col>
                  <v-combobox v-model="editingValue.type" :items="fundTypes" label="Тип"></v-combobox>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div style="display:flex; gap: 10px">
                    <v-btn color="primary" @click="saveModel()">Сохранить</v-btn>
                    <v-btn color="primary" @click="service.createRegular(editingValue)">Обычный</v-btn>
                    <v-btn color="primary" @click="service.createPeriodic(editingValue)">Периодический</v-btn>
                    <v-btn color="primary" @click="service.createTargeted(editingValue)">Целевой</v-btn>
                    <v-btn color="error" @click="editingValue = null">Отменить</v-btn>
                  </div>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-table>
            <thead>
              <tr>
                <th/>
                <th class="text-left">ID</th>
                <th class="text-left">Название</th>
                <th class="text-left">Тип</th>
                <th class="text-left">Закрыт</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="model in fundraisings" :key="model.id">
                <td>
                  <v-icon icon="$edit" @click="editingValue = model"/>
                  <v-icon icon="$delete" @click="deleteModel(model)"/>
                </td>
                <td>{{ model.id }}</td>
                <td>{{ model.name }}</td>
                <td>{{ model.type }}</td>
                <td>{{ model.isClosed }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </v-container>
    <template #fallback> Загрузка... </template>
  </Suspense>
</template>

<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";
import { Fund } from "@/models/fund";
import { Fundraising } from "@/models/fundraising";
import { CrudService } from "@/services/crud-service";
import { FundraisingsService } from "@/services/fundraisings-service";
import { FundraisingEditDto } from "@/models/fundraising-edit-dto";

const fundraisings = ref<Fundraising[]>([]);
const funds = ref<Fund[]>([]);
const service = new FundraisingsService();
const fundsService = new CrudService<Fund>("fund");
const editingValue = ref<FundraisingEditDto | null>(null);

onBeforeMount(async () => {
  funds.value = await fundsService.getAll();
  fundraisings.value = await service.getAll();
});

function addFund(): void {
  editingValue.value = {
    name: "",
    description: "",
    date: new Date(),
    recommendedAmount: 1000,
  }
}

async function saveModel(): Promise<void> {
  const model = editingValue.value!;
  await service.update(model as Fundraising);
}

async function deleteModel(model: Fundraising): Promise<void> {
  await service.delete(model.id);
  fundraisings.value = fundraisings.value.filter((s) => s.id !== model.id);
}
</script>
